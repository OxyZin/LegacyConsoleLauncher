using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1
    {
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

        private string FindGameExe(string rootFolder)
        {
            if (!Directory.Exists(rootFolder))
            {
                return string.Empty;
            }

            string[] files = Directory.GetFiles(rootFolder, "Minecraft.Client.exe", SearchOption.AllDirectories);
            return files.Length > 0 ? files[0] : string.Empty;
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
                var updatedLines = new System.Collections.Generic.List<string>(lines);
                updatedLines.Add("fullscreen=" + (fullscreenCheckBox.Checked ? "1" : "0"));
                File.WriteAllLines(optionsPath, updatedLines.ToArray());
                return;
            }

            File.WriteAllLines(optionsPath, lines);
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
            ApplySkinForAccount(username);

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
    }
}