using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1 : Form
    {
        private string accountsFile = Path.Combine(Application.StartupPath, "accounts.txt");
        private string gamePathFile = Path.Combine(Application.StartupPath, "gamepath.txt");
        private string exePath = Path.Combine(Application.StartupPath, "Minecraft.Client.exe");

        private Dictionary<string, int> playtimeData = new Dictionary<string, int>();
        private DateTime sessionStart;
        private Process gameProcess;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFolderButton.Text = "Open Folder";
            launchButton.Text = "Launch Game";
            setFolderButton.Text = "Set Game Folder";

            fullscreenCheckBox.Text = "Fullscreen";
            usernameLabel.Text = "Username:";
            gamePathLabel.Text = "Game path:";

            gamePathTextBox.ReadOnly = true;
            this.AcceptButton = launchButton;

            LoadAccounts();

            if (File.Exists(gamePathFile))
            {
                string savedPath = File.ReadAllText(gamePathFile).Trim();

                if (File.Exists(savedPath))
                {
                    exePath = savedPath;
                }
            }

            AutoDetectGame();
            UpdateGamePathDisplay();
            UpdatePlaytimeLabel();

            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
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
                Path.Combine(Application.StartupPath, "game", "Minecraft.Client.exe")
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
        }

        private string FormatPlaytime(int totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return ((int)time.TotalHours).ToString() + "h " + time.Minutes.ToString() + "m";
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

            string args = "";

            if (!string.IsNullOrWhiteSpace(username))
            {
                args += "-name \"" + username + "\" ";
            }

            if (fullscreenCheckBox.Checked)
            {
                args += "-fullscreen";
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

            this.Hide();
        }

        private void GameProcess_Exited(object sender, EventArgs e)
        {
            int sessionSeconds = (int)(DateTime.Now - sessionStart).TotalSeconds;

            this.Invoke((MethodInvoker)delegate
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

            usernameComboBox.Items.Clear();
            usernameComboBox.Text = "";
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
    }
}