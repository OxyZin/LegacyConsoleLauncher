using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1
    {
        private void InitializeSkinPreview()
        {
            if (skinPreviewPictureBox == null)
            {
                return;
            }

            skinPreviewPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            UpdateSkinPreview();
        }

        private void UpdateSkinPreview()
        {
            if (skinPreviewPictureBox == null)
            {
                return;
            }

            string username = usernameComboBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                ClearSkinPreview();
                return;
            }

            string skinPath = GetAccountSkinPath(username);

            if (!File.Exists(skinPath))
            {
                ClearSkinPreview();
                return;
            }

            try
            {
                using (Bitmap skin = new Bitmap(skinPath))
                {
                    Bitmap preview = CreateSkinPreviewBitmap(skin);
                    ReplaceSkinPreviewImage(preview);
                }
            }
            catch
            {
                ClearSkinPreview();
            }
        }

        private void ClearSkinPreview()
        {
            if (skinPreviewPictureBox.Image != null)
            {
                skinPreviewPictureBox.Image.Dispose();
                skinPreviewPictureBox.Image = null;
            }
        }

        private void ReplaceSkinPreviewImage(Image newImage)
        {
            if (skinPreviewPictureBox.Image != null)
            {
                skinPreviewPictureBox.Image.Dispose();
            }

            skinPreviewPictureBox.Image = newImage;
        }

        private Bitmap CreateSkinPreviewBitmap(Bitmap skin)
        {
            // base paper doll canvas
            Bitmap preview = new Bitmap(16, 32, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(preview))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;

                // Head front: source (8,8) size 8x8
                g.DrawImage(
                    skin,
                    new Rectangle(4, 0, 8, 8),
                    new Rectangle(8, 8, 8, 8),
                    GraphicsUnit.Pixel
                );

                // Body front: source (20,20) size 8x12
                g.DrawImage(
                    skin,
                    new Rectangle(4, 8, 8, 12),
                    new Rectangle(20, 20, 8, 12),
                    GraphicsUnit.Pixel
                );

                // Right arm front: source (44,20) size 4x12
                g.DrawImage(
                    skin,
                    new Rectangle(0, 8, 4, 12),
                    new Rectangle(44, 20, 4, 12),
                    GraphicsUnit.Pixel
                );

                // Left arm front (legacy format reuses arm texture)
                g.DrawImage(
                    skin,
                    new Rectangle(12, 8, 4, 12),
                    new Rectangle(44, 20, 4, 12),
                    GraphicsUnit.Pixel
                );

                // Right leg front: source (4,20) size 4x12
                g.DrawImage(
                    skin,
                    new Rectangle(4, 20, 4, 12),
                    new Rectangle(4, 20, 4, 12),
                    GraphicsUnit.Pixel
                );

                // Left leg front (legacy format reuses leg texture)
                g.DrawImage(
                    skin,
                    new Rectangle(8, 20, 4, 12),
                    new Rectangle(4, 20, 4, 12),
                    GraphicsUnit.Pixel
                );
            }

            // upscale for cleaner display
            Bitmap scaled = new Bitmap(96, 192, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(scaled))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(preview, new Rectangle(0, 0, scaled.Width, scaled.Height));
            }

            preview.Dispose();
            return scaled;
        }
    }
}