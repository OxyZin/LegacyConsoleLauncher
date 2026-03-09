using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1 : Form
    {
        private readonly string accountsFile = Path.Combine(Application.StartupPath, "accounts.txt");
        private readonly string gamePathFile = Path.Combine(Application.StartupPath, "gamepath.txt");
        private readonly string releaseInfoFile = Path.Combine(Application.StartupPath, "releaseinfo.txt");
        private readonly string gameInstallDir = Path.Combine(Application.StartupPath, "Game");

        private string exePath = Path.Combine(Application.StartupPath, "Minecraft.Client.exe");

        private readonly string nightlyReleaseUrl = "https://github.com/smartcmd/MinecraftConsoles/releases/tag/nightly";
        private readonly string nightlyZipUrl = "https://github.com/smartcmd/MinecraftConsoles/releases/download/nightly/LCEWindows64.zip";
        private readonly string nightlyExeUrl = "https://github.com/smartcmd/MinecraftConsoles/releases/download/nightly/Minecraft.Client.exe";

        private readonly Dictionary<string, int> playtimeData = new Dictionary<string, int>();
        private DateTime sessionStart;
        private Process gameProcess;
        private Form2 progressForm;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            openFolderButton.Text = "Open Folder";
            launchButton.Text = "Launch Game";
            setFolderButton.Text = "Set Game Folder";

            fullscreenCheckBox.Text = "Fullscreen";
            usernameLabel.Text = "Username:";
            gamePathLabel.Text = "Game path:";

            gamePathTextBox.ReadOnly = true;
            AcceptButton = launchButton;
            checkforLink.Visible = false;

            LoadAccounts();
            LoadSavedGamePath();
            AutoDetectGame();
            UpdateGamePathDisplay();
            LoadFullscreenSetting();
            UpdatePlaytimeLabel();

            AllowDrop = true;
            DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;

            await CheckForUpdatesOnStartupAsync();
        }

        private void LoadSavedGamePath()
        {
            if (!File.Exists(gamePathFile))
            {
                return;
            }

            string savedPath = File.ReadAllText(gamePathFile).Trim();

            if (File.Exists(savedPath))
            {
                exePath = savedPath;
            }
        }

        private void LoadAccounts()
        {
            usernameComboBox.Items.Clear();
            playtimeData.Clear();

            if (!File.Exists(accountsFile))
            {
                return;
            }

            string[] lines = File.ReadAllLines(accountsFile);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                string username = parts[0].Trim();

                if (string.IsNullOrWhiteSpace(username))
                {
                    continue;
                }

                int seconds = 0;

                if (parts.Length > 1)
                {
                    int.TryParse(parts[1], out seconds);
                }

                if (!playtimeData.ContainsKey(username))
                {
                    playtimeData[username] = seconds;
                    usernameComboBox.Items.Add(username);
                }
            }

            if (usernameComboBox.Items.Count > 0)
            {
                usernameComboBox.SelectedIndex = 0;
            }
        }

        private void SaveAccounts()
        {
            List<string> lines = new List<string>();

            foreach (var entry in playtimeData)
            {
                lines.Add(entry.Key + "|" + entry.Value);
            }

            File.WriteAllLines(accountsFile, lines);
        }

        private void AddAccount(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            if (!playtimeData.ContainsKey(username))
            {
                playtimeData[username] = 0;
                usernameComboBox.Items.Add(username);
            }

            usernameComboBox.SelectedItem = username;
            SaveAccounts();
        }

        private void AutoDetectGame()
        {
            if (File.Exists(exePath))
            {
                return;
            }

            string[] possiblePaths =
            {
                Path.Combine(Application.StartupPath, "Minecraft.Client.exe"),
                Path.Combine(Application.StartupPath, "build", "Minecraft.Client.exe"),
                Path.Combine(Application.StartupPath, "bin", "Minecraft.Client.exe"),
                Path.Combine(Application.StartupPath, "game", "Minecraft.Client.exe"),
                Path.Combine(gameInstallDir, "Minecraft.Client.exe")
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    exePath = path;
                    File.WriteAllText(gamePathFile, exePath);
                    break;
                }
            }
        }

        private void UpdateGamePathDisplay()
        {
            if (File.Exists(exePath))
            {
                gamePathTextBox.Text = exePath;
            }
            else
            {
                gamePathTextBox.Text = "Not set";
            }

            bool gameFound = File.Exists(exePath);
            openFolderButton.Enabled = gameFound;
            launchButton.Enabled = gameFound;
        }

        private void SetGamePath(string newPath)
        {
            exePath = newPath;
            File.WriteAllText(gamePathFile, exePath);
            UpdateGamePathDisplay();
            LoadFullscreenSetting();
        }

        private string FormatPlaytime(int totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return ((int)time.TotalHours) + "h " + time.Minutes + "m";
        }

        private void UpdatePlaytimeLabel()
        {
            string username = usernameComboBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                playtimeLabel.Text = "Playtime: 0h 0m";
                return;
            }

            if (!playtimeData.ContainsKey(username))
            {
                playtimeData[username] = 0;
            }

            playtimeLabel.Text = "Playtime: " + FormatPlaytime(playtimeData[username]);
        }

        private void LoadFullscreenSetting()
        {
            if (!File.Exists(exePath))
            {
                return;
            }

            string gameFolder = Path.GetDirectoryName(exePath);
            string optionsPath = Path.Combine(gameFolder, "options.txt");

            if (!File.Exists(optionsPath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(optionsPath);

            foreach (string line in lines)
            {
                if (line.StartsWith("fullscreen="))
                {
                    fullscreenCheckBox.Checked = line.Trim() == "fullscreen=1";
                    break;
                }
            }
        }

        private void SaveFullscreenSetting()
        {
            if (!File.Exists(exePath))
            {
                return;
            }

            string gameFolder = Path.GetDirectoryName(exePath);
            string optionsPath = Path.Combine(gameFolder, "options.txt");

            if (!File.Exists(optionsPath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(optionsPath);
            bool foundFullscreen = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("fullscreen="))
                {
                    lines[i] = "fullscreen=" + (fullscreenCheckBox.Checked ? "1" : "0");
                    foundFullscreen = true;
                    break;
                }
            }

            if (!foundFullscreen)
            {
                List<string> updatedLines = new List<string>(lines);
                updatedLines.Add("fullscreen=" + (fullscreenCheckBox.Checked ? "1" : "0"));
                File.WriteAllLines(optionsPath, updatedLines.ToArray());
                return;
            }

            File.WriteAllLines(optionsPath, lines);
        }

        private string FindGameExe(string rootFolder)
        {
            if (!Directory.Exists(rootFolder))
            {
                return string.Empty;
            }

            string[] files = Directory.GetFiles(rootFolder, "Minecraft.Client.exe", SearchOption.AllDirectories);
            return files.Length > 0 ? files[0] : string.Empty;
        }

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

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(exePath))
            {
                MessageBox.Show(
                    "Minecraft.Client.exe was not found.",
                    "Game Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string gameFolder = Path.GetDirectoryName(exePath);
            Process.Start("explorer.exe", gameFolder);
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(exePath))
            {
                MessageBox.Show(
                    "Minecraft.Client.exe was not found.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            string username = usernameComboBox.Text.Trim();

            AddAccount(username);
            File.WriteAllText(gamePathFile, exePath);
            SaveFullscreenSetting();

            string args = string.Empty;

            if (!string.IsNullOrWhiteSpace(username))
            {
                args += "-name \"" + username + "\" ";
            }

            gameProcess = Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = args.Trim(),
                WorkingDirectory = Path.GetDirectoryName(exePath),
                UseShellExecute = true
            });

            if (gameProcess == null)
            {
                MessageBox.Show(
                    "Failed to start the game.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            sessionStart = DateTime.Now;
            gameProcess.EnableRaisingEvents = true;
            gameProcess.Exited += GameProcess_Exited;

            Hide();
        }

        private void GameProcess_Exited(object sender, EventArgs e)
        {
            int sessionSeconds = (int)(DateTime.Now - sessionStart).TotalSeconds;

            Invoke((MethodInvoker)delegate
            {
                string username = usernameComboBox.Text.Trim();

                if (!string.IsNullOrWhiteSpace(username))
                {
                    if (!playtimeData.ContainsKey(username))
                    {
                        playtimeData[username] = 0;
                    }

                    playtimeData[username] += sessionSeconds;
                    SaveAccounts();
                }

                Application.Exit();
            });
        }

        private void setFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the folder containing Minecraft.Client.exe";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = dialog.SelectedPath;
                    string possibleExe = Path.Combine(selectedFolder, "Minecraft.Client.exe");

                    if (File.Exists(possibleExe))
                    {
                        SetGamePath(possibleExe);

                        MessageBox.Show(
                            "Game folder set successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "Minecraft.Client.exe was not found in that folder.",
                            "Game Not Found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (paths.Length == 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            string droppedPath = paths[0];

            if (File.Exists(droppedPath))
            {
                if (Path.GetFileName(droppedPath).Equals("Minecraft.Client.exe", StringComparison.OrdinalIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }

            if (Directory.Exists(droppedPath))
            {
                string possibleExe = Path.Combine(droppedPath, "Minecraft.Client.exe");

                if (File.Exists(possibleExe))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }

            e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (paths.Length == 0)
            {
                return;
            }

            string droppedPath = paths[0];

            if (File.Exists(droppedPath))
            {
                if (Path.GetFileName(droppedPath).Equals("Minecraft.Client.exe", StringComparison.OrdinalIgnoreCase))
                {
                    SetGamePath(droppedPath);

                    MessageBox.Show(
                        "Minecraft.Client.exe loaded successfully.",
                        "Game Selected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }
            }

            if (Directory.Exists(droppedPath))
            {
                string possibleExe = Path.Combine(droppedPath, "Minecraft.Client.exe");

                if (File.Exists(possibleExe))
                {
                    SetGamePath(possibleExe);

                    MessageBox.Show(
                        "Game folder loaded successfully.",
                        "Game Selected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }
            }

            MessageBox.Show(
                "Please drag and drop either Minecraft.Client.exe or the folder containing it.",
                "Invalid Drop",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        private void resetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "This will reset the saved accounts and game path. Continue?",
                "Reset Launcher Settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            if (File.Exists(accountsFile))
            {
                File.Delete(accountsFile);
            }

            if (File.Exists(gamePathFile))
            {
                File.Delete(gamePathFile);
            }

            if (File.Exists(releaseInfoFile))
            {
                File.Delete(releaseInfoFile);
            }

            usernameComboBox.Items.Clear();
            usernameComboBox.Text = string.Empty;
            playtimeData.Clear();

            exePath = Path.Combine(Application.StartupPath, "Minecraft.Client.exe");
            fullscreenCheckBox.Checked = false;

            AutoDetectGame();
            UpdateGamePathDisplay();
            UpdatePlaytimeLabel();

            MessageBox.Show(
                "Launcher settings have been reset.",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void usernameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlaytimeLabel();
        }

        private void fullscreenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void usernameLabel_Click(object sender, EventArgs e)
        {
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void gamePathLabel_Click(object sender, EventArgs e)
        {
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