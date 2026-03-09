using System;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1
    {
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
            UpdateSkinPreview();
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