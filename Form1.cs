using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1 : Form
    {
        private readonly string accountsFile = LauncherPaths.AccountsFile;
        private readonly string gamePathFile = LauncherPaths.GamePathFile;
        private readonly string releaseInfoFile = LauncherPaths.ReleaseInfoFile;
        private readonly string gameInstallDir = LauncherPaths.GameDir;
        private readonly string skinsDir = LauncherPaths.SkinsDir;

        private string exePath = Path.Combine(LauncherPaths.GameDir, "Minecraft.Client.exe");

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
            button1.Text = "Choose Skin";

            fullscreenCheckBox.Text = "Fullscreen";
            usernameLabel.Text = "Username:";
            gamePathLabel.Text = "Game path:";

            gamePathTextBox.ReadOnly = true;
            AcceptButton = launchButton;
            checkforLink.Visible = false;

            Directory.CreateDirectory(LauncherPaths.DataDir);
            Directory.CreateDirectory(LauncherPaths.GameDir);
            Directory.CreateDirectory(LauncherPaths.SkinsDir);

            LoadAccounts();
            LoadSavedGamePath();
            AutoDetectGame();
            UpdateGamePathDisplay();
            LoadFullscreenSetting();
            UpdatePlaytimeLabel();
            InitializeSkinPreview();

            AllowDrop = true;
            DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;

            SettingsManager.Load();

            if (SettingsManager.CheckGameUpdatesOnStartup)
            {
                await CheckForUpdatesOnStartupAsync();
            }
        }
    }
}