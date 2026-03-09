using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1
    {
        private async Task<string> DownloadStringWithUserAgentAsync(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "LegacyConsoleLauncher");
                return await client.DownloadStringTaskAsync(url);
            }
        }

        private async Task DownloadFileWithProgressAsync(string url, string outputPath, string statusText)
        {
            ShowProgressForm(statusText);

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "LegacyConsoleLauncher");

                client.DownloadProgressChanged += (s, e) =>
                {
                    if (progressForm != null && !progressForm.IsDisposed)
                    {
                        progressForm.SetStatus(statusText + " " + e.ProgressPercentage + "%");
                        progressForm.SetProgress(e.ProgressPercentage);
                    }
                };

                await client.DownloadFileTaskAsync(new Uri(url), outputPath);
            }

            if (progressForm != null && !progressForm.IsDisposed)
            {
                progressForm.SetProgress(100);
            }
        }

        private void ShowProgressForm(string status)
        {
            if (progressForm == null || progressForm.IsDisposed)
            {
                progressForm = new Form2();
            }

            progressForm.Show();
            progressForm.BringToFront();
            progressForm.SetStatus(status);
            progressForm.SetProgress(0);
        }

        private void CloseProgressForm()
        {
            if (progressForm != null && !progressForm.IsDisposed)
            {
                progressForm.Close();
            }
        }

        private async Task<string> GetNightlyCommitAsync()
        {
            string html = await DownloadStringWithUserAgentAsync(nightlyReleaseUrl);

            Match match = Regex.Match(
                html,
                @"\b[0-9a-f]{7,40}\b",
                RegexOptions.IgnoreCase
            );

            return match.Success ? match.Value : string.Empty;
        }

        private string GetInstalledCommit()
        {
            if (!File.Exists(releaseInfoFile))
            {
                return string.Empty;
            }

            foreach (string line in File.ReadAllLines(releaseInfoFile))
            {
                if (line.StartsWith("commit="))
                {
                    return line.Substring("commit=".Length).Trim();
                }
            }

            return string.Empty;
        }

        private void SaveInstalledCommit(string commit)
        {
            if (string.IsNullOrWhiteSpace(commit))
            {
                return;
            }

            File.WriteAllText(releaseInfoFile, "commit=" + commit);
        }

        private async Task InstallGameAsync()
        {
            string zipPath = Path.Combine(Application.StartupPath, "LCEWindows64.zip");
            string tempExtractDir = Path.Combine(Application.StartupPath, "Game_Temp");

            try
            {
                await DownloadFileWithProgressAsync(nightlyZipUrl, zipPath, "Installing...");
                ShowProgressForm("Extracting...");

                if (Directory.Exists(tempExtractDir))
                {
                    Directory.Delete(tempExtractDir, true);
                }

                Directory.CreateDirectory(tempExtractDir);
                ZipFile.ExtractToDirectory(zipPath, tempExtractDir);

                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                if (Directory.Exists(gameInstallDir))
                {
                    Directory.Delete(gameInstallDir, true);
                }

                Directory.Move(tempExtractDir, gameInstallDir);

                string detectedExe = FindGameExe(gameInstallDir);

                if (string.IsNullOrWhiteSpace(detectedExe) || !File.Exists(detectedExe))
                {
                    throw new FileNotFoundException("Minecraft.Client.exe could not be found after installation.");
                }

                SetGamePath(detectedExe);

                string latestCommit = await GetNightlyCommitAsync();
                SaveInstalledCommit(latestCommit);

                CloseProgressForm();

                MessageBox.Show(
                    "Game installed successfully.",
                    "Install Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch
            {
                CloseProgressForm();

                if (Directory.Exists(tempExtractDir))
                {
                    Directory.Delete(tempExtractDir, true);
                }

                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                throw;
            }
        }

        private async Task UpdateGameExeAsync()
        {
            if (!File.Exists(exePath))
            {
                throw new FileNotFoundException("Game executable was not found.");
            }

            string tempExePath = Path.Combine(Application.StartupPath, "Minecraft.Client.new.exe");

            try
            {
                await DownloadFileWithProgressAsync(nightlyExeUrl, tempExePath, "Updating...");
                File.Copy(tempExePath, exePath, true);

                if (File.Exists(tempExePath))
                {
                    File.Delete(tempExePath);
                }

                string latestCommit = await GetNightlyCommitAsync();
                SaveInstalledCommit(latestCommit);

                CloseProgressForm();

                MessageBox.Show(
                    "Game updated successfully.",
                    "Update Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch
            {
                CloseProgressForm();

                if (File.Exists(tempExePath))
                {
                    File.Delete(tempExePath);
                }

                throw;
            }
        }

        private async Task CheckForUpdatesOnStartupAsync()
        {
            checkforLink.Visible = false;

            try
            {
                string latestCommit = await GetNightlyCommitAsync();
                string installedCommit = GetInstalledCommit();

                if (string.IsNullOrWhiteSpace(latestCommit))
                {
                    return;
                }

                if (!File.Exists(exePath))
                {
                    DialogResult installResult = MessageBox.Show(
                        "Game not installed.\n\nDo you want to download and install it now?",
                        "Install Game",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (installResult == DialogResult.Yes)
                    {
                        await InstallGameAsync();
                    }

                    checkforLink.Visible = true;
                    return;
                }

                if (!string.Equals(latestCommit, installedCommit, StringComparison.OrdinalIgnoreCase))
                {
                    checkforLink.Visible = true;

                    DialogResult updateResult = MessageBox.Show(
                        "A new nightly build is available.\n\nDo you want to update now?",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (updateResult == DialogResult.Yes)
                    {
                        await UpdateGameExeAsync();
                        checkforLink.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                checkforLink.Visible = true;

                MessageBox.Show(
                    "Failed to check for updates.\n\n" + ex.Message,
                    "Update Check Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private async void checkforLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string latestCommit = await GetNightlyCommitAsync();
                string installedCommit = GetInstalledCommit();

                if (!File.Exists(exePath))
                {
                    await InstallGameAsync();
                    checkforLink.Visible = false;
                    return;
                }

                if (!string.Equals(latestCommit, installedCommit, StringComparison.OrdinalIgnoreCase))
                {
                    await UpdateGameExeAsync();
                    checkforLink.Visible = false;
                }
                else
                {
                    MessageBox.Show(
                        "You are already using the latest nightly build.",
                        "No Updates",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to check for updates.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}