namespace LegacyConsoleLauncher
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.launchButton = new System.Windows.Forms.Button();
            this.fullscreenCheckBox = new System.Windows.Forms.CheckBox();
            this.usernameComboBox = new System.Windows.Forms.ComboBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.setFolderButton = new System.Windows.Forms.Button();
            this.gamePathLabel = new System.Windows.Forms.Label();
            this.gamePathTextBox = new System.Windows.Forms.TextBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.resetLink = new System.Windows.Forms.LinkLabel();
            this.playtimeLabel = new System.Windows.Forms.Label();
            this.checkforLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // launchButton
            // 
            this.launchButton.Location = new System.Drawing.Point(246, 224);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(111, 32);
            this.launchButton.TabIndex = 1;
            this.launchButton.Text = "Launch Game";
            this.launchButton.UseVisualStyleBackColor = true;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // fullscreenCheckBox
            // 
            this.fullscreenCheckBox.AutoSize = true;
            this.fullscreenCheckBox.Location = new System.Drawing.Point(291, 119);
            this.fullscreenCheckBox.Name = "fullscreenCheckBox";
            this.fullscreenCheckBox.Size = new System.Drawing.Size(74, 17);
            this.fullscreenCheckBox.TabIndex = 2;
            this.fullscreenCheckBox.Text = "Fullscreen";
            this.fullscreenCheckBox.UseVisualStyleBackColor = true;
            this.fullscreenCheckBox.CheckedChanged += new System.EventHandler(this.fullscreenCheckBox_CheckedChanged);
            // 
            // usernameComboBox
            // 
            this.usernameComboBox.FormattingEnabled = true;
            this.usernameComboBox.Location = new System.Drawing.Point(12, 117);
            this.usernameComboBox.Name = "usernameComboBox";
            this.usernameComboBox.Size = new System.Drawing.Size(274, 21);
            this.usernameComboBox.TabIndex = 3;
            this.usernameComboBox.SelectedIndexChanged += new System.EventHandler(this.usernameComboBox_SelectedIndexChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(9, 102);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "Username";
            this.usernameLabel.Click += new System.EventHandler(this.usernameLabel_Click);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(27, 19);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(309, 63);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 5;
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.Click += new System.EventHandler(this.logoPictureBox_Click);
            // 
            // setFolderButton
            // 
            this.setFolderButton.Location = new System.Drawing.Point(129, 224);
            this.setFolderButton.Name = "setFolderButton";
            this.setFolderButton.Size = new System.Drawing.Size(111, 32);
            this.setFolderButton.TabIndex = 6;
            this.setFolderButton.Text = "Set Game Folder";
            this.setFolderButton.UseVisualStyleBackColor = true;
            this.setFolderButton.Click += new System.EventHandler(this.setFolderButton_Click);
            // 
            // gamePathLabel
            // 
            this.gamePathLabel.AutoSize = true;
            this.gamePathLabel.Location = new System.Drawing.Point(13, 156);
            this.gamePathLabel.Name = "gamePathLabel";
            this.gamePathLabel.Size = new System.Drawing.Size(62, 13);
            this.gamePathLabel.TabIndex = 7;
            this.gamePathLabel.Text = "Game path:";
            this.gamePathLabel.Click += new System.EventHandler(this.gamePathLabel_Click);
            // 
            // gamePathTextBox
            // 
            this.gamePathTextBox.Location = new System.Drawing.Point(12, 172);
            this.gamePathTextBox.Name = "gamePathTextBox";
            this.gamePathTextBox.Size = new System.Drawing.Size(345, 20);
            this.gamePathTextBox.TabIndex = 8;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(12, 224);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(111, 32);
            this.openFolderButton.TabIndex = 9;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // resetLink
            // 
            this.resetLink.AutoSize = true;
            this.resetLink.Location = new System.Drawing.Point(13, 280);
            this.resetLink.Name = "resetLink";
            this.resetLink.Size = new System.Drawing.Size(124, 13);
            this.resetLink.TabIndex = 10;
            this.resetLink.TabStop = true;
            this.resetLink.Text = "Reset Launcher Settings";
            this.resetLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.resetLink_LinkClicked);
            // 
            // playtimeLabel
            // 
            this.playtimeLabel.AutoSize = true;
            this.playtimeLabel.Location = new System.Drawing.Point(264, 280);
            this.playtimeLabel.Name = "playtimeLabel";
            this.playtimeLabel.Size = new System.Drawing.Size(81, 13);
            this.playtimeLabel.TabIndex = 11;
            this.playtimeLabel.Text = "Playtime: 0h 0m";
            // 
            // checkforLink
            // 
            this.checkforLink.AutoSize = true;
            this.checkforLink.Location = new System.Drawing.Point(143, 280);
            this.checkforLink.Name = "checkforLink";
            this.checkforLink.Size = new System.Drawing.Size(94, 13);
            this.checkforLink.TabIndex = 12;
            this.checkforLink.TabStop = true;
            this.checkforLink.Text = "Check for updates";
            this.checkforLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkforLink_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 302);
            this.Controls.Add(this.checkforLink);
            this.Controls.Add(this.resetLink);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.gamePathTextBox);
            this.Controls.Add(this.gamePathLabel);
            this.Controls.Add(this.setFolderButton);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.usernameComboBox);
            this.Controls.Add(this.fullscreenCheckBox);
            this.Controls.Add(this.launchButton);
            this.Controls.Add(this.playtimeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "LegacyConsoleLauncher";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.CheckBox fullscreenCheckBox;
        private System.Windows.Forms.ComboBox usernameComboBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Button setFolderButton;
        private System.Windows.Forms.Label gamePathLabel;
        private System.Windows.Forms.TextBox gamePathTextBox;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.LinkLabel resetLink;
        private System.Windows.Forms.Label playtimeLabel;
        private System.Windows.Forms.LinkLabel checkforLink;
    }
}