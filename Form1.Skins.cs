using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1
    {
        private string GetSkinFolder()
        {
            return skinsDir;
        }

        private string GetAccountSkinPath(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return string.Empty;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                username = username.Replace(c, '_');
            }

            return Path.Combine(GetSkinFolder(), username + ".png");
        }

        private string GetGameRootFolder()
        {
            if (!File.Exists(exePath))
            {
                return string.Empty;
            }

            return Path.GetDirectoryName(exePath);
        }

        private string GetGameSkinFolder()
        {
            string gameRoot = GetGameRootFolder();

            if (string.IsNullOrWhiteSpace(gameRoot))
            {
                return string.Empty;
            }

            return Path.Combine(gameRoot, "Common", "res", "mob");
        }

        private string GetGameSkinPath()
        {
            string skinFolder = GetGameSkinFolder();

            if (string.IsNullOrWhiteSpace(skinFolder))
            {
                return string.Empty;
            }

            return Path.Combine(skinFolder, "char.png");
        }

        private string GetOriginalSkinBackupPath()
        {
            string skinFolder = GetGameSkinFolder();

            if (string.IsNullOrWhiteSpace(skinFolder))
            {
                return string.Empty;
            }

            return Path.Combine(skinFolder, "char_original_backup.png");
        }

        private void EnsureOriginalSkinBackup()
        {
            string gameSkinPath = GetGameSkinPath();
            string backupPath = GetOriginalSkinBackupPath();

            if (string.IsNullOrWhiteSpace(gameSkinPath) || string.IsNullOrWhiteSpace(backupPath))
            {
                return;
            }

            if (!File.Exists(gameSkinPath))
            {
                return;
            }

            if (!File.Exists(backupPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(backupPath));
                File.Copy(gameSkinPath, backupPath);
            }
        }

        private Bitmap ConvertSkinToLegacyFormat(Bitmap original)
        {
            if (original.Width != 64 || (original.Height != 32 && original.Height != 64))
            {
                return null;
            }

            if (original.Height == 32)
            {
                return new Bitmap(original);
            }

            Bitmap converted = new Bitmap(64, 32, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(converted))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(
                    original,
                    new Rectangle(0, 0, 64, 32),
                    new Rectangle(0, 0, 64, 32),
                    GraphicsUnit.Pixel
                );
            }

            return converted;
        }

        private void SaveSkinForCurrentAccount()
        {
            string username = usernameComboBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show(
                    "Please enter or select an account first.",
                    "Skin Manager",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                UpdateSkinPreview();
                return;
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "PNG Files (*.png)|*.png";
                dialog.Title = "Select a Minecraft skin";

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                using (Bitmap original = new Bitmap(dialog.FileName))
                using (Bitmap converted = ConvertSkinToLegacyFormat(original))
                {
                    if (converted == null)
                    {
                        MessageBox.Show(
                            "Invalid skin format. Only 64x32 and 64x64 PNG skins are supported.",
                            "Invalid Skin",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    Directory.CreateDirectory(GetSkinFolder());
                    string accountSkinPath = GetAccountSkinPath(username);
                    converted.Save(accountSkinPath, ImageFormat.Png);

                    string formatMessage = original.Height == 64
                        ? "Skin saved for this account.\n\n64x64 skin was converted to 64x32."
                        : "Skin saved for this account.";

                    MessageBox.Show(
                        formatMessage,
                        "Skin Manager",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private void ApplySkinForAccount(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                RestoreOriginalSkin();
                return;
            }

            string accountSkinPath = GetAccountSkinPath(username);
            string gameSkinPath = GetGameSkinPath();

            if (string.IsNullOrWhiteSpace(gameSkinPath))
            {
                return;
            }

            if (!File.Exists(accountSkinPath))
            {
                RestoreOriginalSkin();
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(gameSkinPath));
            EnsureOriginalSkinBackup();
            File.Copy(accountSkinPath, gameSkinPath, true);
        }

        private void RestoreOriginalSkin()
        {
            string backupPath = GetOriginalSkinBackupPath();
            string gameSkinPath = GetGameSkinPath();

            if (string.IsNullOrWhiteSpace(backupPath) || string.IsNullOrWhiteSpace(gameSkinPath))
            {
                return;
            }

            if (!File.Exists(backupPath))
            {
                return;
            }

            File.Copy(backupPath, gameSkinPath, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSkinForCurrentAccount();
        }
    }
}