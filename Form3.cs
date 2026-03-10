using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form3 : Form
    {
        private readonly string launcherDataDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LCELauncher");

        private readonly string skinsDir;

        public Form3()
        {
            InitializeComponent();
            skinsDir = Path.Combine(launcherDataDir, "skins");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(launcherDataDir);
            Directory.CreateDirectory(skinsDir);

            versionLabel.Text = "Legacy Console Launcher v" + Application.ProductVersion;

            SettingsManager.Load();

            checkGameUpdatesCheckBox.Checked = SettingsManager.CheckGameUpdatesOnStartup;
            checkLauncherUpdatesCheckBox.Checked = SettingsManager.CheckLauncherUpdatesOnStartup;
            launchArgsTextBox.Text = SettingsManager.AdditionalLaunchArgs;
        }

        private void SaveUiToSettings()
        {
            SettingsManager.CheckGameUpdatesOnStartup = checkGameUpdatesCheckBox.Checked;
            SettingsManager.CheckLauncherUpdatesOnStartup = checkLauncherUpdatesCheckBox.Checked;
            SettingsManager.AdditionalLaunchArgs = launchArgsTextBox.Text.Trim();

            SettingsManager.Save();
        }

        private void openDataFolderButton_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(launcherDataDir);
            Process.Start("explorer.exe", launcherDataDir);
        }

        private void openSkinsFolderButton_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(skinsDir);
            Process.Start("explorer.exe", skinsDir);
        }

        private void ResetEverything()
        {
            Form1 mainForm = this.Owner as Form1;

            if (mainForm == null)
            {
                MessageBox.Show("Main launcher window not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mainForm.ResetLauncherData();
            SettingsManager.Reset();
        }

        private void resetLauncherSettingsButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "This will reset all launcher data and settings. Continue?",
                "Reset Launcher",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            ResetEverything();

            checkGameUpdatesCheckBox.Checked = SettingsManager.CheckGameUpdatesOnStartup;
            checkLauncherUpdatesCheckBox.Checked = SettingsManager.CheckLauncherUpdatesOnStartup;
            launchArgsTextBox.Text = SettingsManager.AdditionalLaunchArgs;

            MessageBox.Show(
                "Launcher has been fully reset.",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void githubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/OxyZin/LegacyConsoleLauncher",
                UseShellExecute = true
            });
        }

        private void checkGameUpdatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkLauncherUpdatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void launchArgsTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveUiToSettings();
            this.Close();
        }
    }
}