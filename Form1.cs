using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1 : Form
    {
        string usernameFile = Path.Combine(Application.StartupPath, "username.txt");
        string gamePathFile = Path.Combine(Application.StartupPath, "gamepath.txt");
        string exePath = Path.Combine(Application.StartupPath, "Minecraft.Client.exe");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Text = "Launch!";
            button3.Text = "Set Game Folder";

            checkBox1.Text = "Fullscreen";
            label1.Text = "Username:";

            if (File.Exists(usernameFile))
            {
                textBox1.Text = File.ReadAllText(usernameFile).Trim();
            }

            if (File.Exists(gamePathFile))
            {
                string savedPath = File.ReadAllText(gamePathFile).Trim();

                if (File.Exists(savedPath))
                {
                    exePath = savedPath;
                }
            }

            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
        }

        private void button2_Click(object sender, EventArgs e)
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

            File.WriteAllText(usernameFile, textBox1.Text.Trim());
            File.WriteAllText(gamePathFile, exePath);

            string args = "";

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                args += "-name \"" + textBox1.Text.Trim() + "\" ";
            }

            if (checkBox1.Checked)
            {
                args += "-fullscreen";
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = args.Trim(),
                WorkingDirectory = Path.GetDirectoryName(exePath),
                UseShellExecute = true
            });

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
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
                        exePath = possibleExe;
                        File.WriteAllText(gamePathFile, exePath);

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
                    exePath = droppedPath;
                    File.WriteAllText(gamePathFile, exePath);

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
                    exePath = possibleExe;
                    File.WriteAllText(gamePathFile, exePath);

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}