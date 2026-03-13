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
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.setFolderButton = new System.Windows.Forms.Button();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.playtimeLabel = new System.Windows.Forms.Label();
            this.checkforLink = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.skinPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gamePathTextBox = new System.Windows.Forms.TextBox();
            this.gamePathLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newsFlow = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPreviewPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // launchButton
            // 
            this.launchButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.launchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launchButton.Location = new System.Drawing.Point(748, 8);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(84, 22);
            this.launchButton.TabIndex = 1;
            this.launchButton.Text = "Launch Game";
            this.launchButton.UseVisualStyleBackColor = false;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // fullscreenCheckBox
            // 
            this.fullscreenCheckBox.AutoSize = true;
            this.fullscreenCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.fullscreenCheckBox.Location = new System.Drawing.Point(363, 443);
            this.fullscreenCheckBox.Name = "fullscreenCheckBox";
            this.fullscreenCheckBox.Size = new System.Drawing.Size(74, 17);
            this.fullscreenCheckBox.TabIndex = 2;
            this.fullscreenCheckBox.Text = "Fullscreen";
            this.fullscreenCheckBox.UseVisualStyleBackColor = false;
            this.fullscreenCheckBox.CheckedChanged += new System.EventHandler(this.fullscreenCheckBox_CheckedChanged);
            // 
            // usernameComboBox
            // 
            this.usernameComboBox.FormattingEnabled = true;
            this.usernameComboBox.Location = new System.Drawing.Point(472, 391);
            this.usernameComboBox.Name = "usernameComboBox";
            this.usernameComboBox.Size = new System.Drawing.Size(274, 21);
            this.usernameComboBox.TabIndex = 3;
            this.usernameComboBox.SelectedIndexChanged += new System.EventHandler(this.usernameComboBox_SelectedIndexChanged);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(12, 14);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(309, 63);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 5;
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.Click += new System.EventHandler(this.logoPictureBox_Click);
            // 
            // setFolderButton
            // 
            this.setFolderButton.Location = new System.Drawing.Point(553, 58);
            this.setFolderButton.Name = "setFolderButton";
            this.setFolderButton.Size = new System.Drawing.Size(95, 21);
            this.setFolderButton.TabIndex = 6;
            this.setFolderButton.Text = "Set Game Folder";
            this.setFolderButton.UseVisualStyleBackColor = true;
            this.setFolderButton.Click += new System.EventHandler(this.setFolderButton_Click);
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(472, 58);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(78, 21);
            this.openFolderButton.TabIndex = 9;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // playtimeLabel
            // 
            this.playtimeLabel.AutoSize = true;
            this.playtimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.playtimeLabel.ForeColor = System.Drawing.Color.White;
            this.playtimeLabel.Location = new System.Drawing.Point(748, 58);
            this.playtimeLabel.Name = "playtimeLabel";
            this.playtimeLabel.Size = new System.Drawing.Size(81, 13);
            this.playtimeLabel.TabIndex = 11;
            this.playtimeLabel.Text = "Playtime: 0h 0m";
            // 
            // checkforLink
            // 
            this.checkforLink.AutoSize = true;
            this.checkforLink.BackColor = System.Drawing.Color.Transparent;
            this.checkforLink.LinkColor = System.Drawing.Color.Turquoise;
            this.checkforLink.Location = new System.Drawing.Point(12, 74);
            this.checkforLink.Name = "checkforLink";
            this.checkforLink.Size = new System.Drawing.Size(75, 13);
            this.checkforLink.TabIndex = 12;
            this.checkforLink.TabStop = true;
            this.checkforLink.Text = "Install updates";
            this.checkforLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkforLink_LinkClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(708, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 32);
            this.button1.TabIndex = 13;
            this.button1.Text = "Choose Skin";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(650, 58);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 21);
            this.button3.TabIndex = 15;
            this.button3.Text = "Servers (WIP)";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // skinPreviewPictureBox
            // 
            this.skinPreviewPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.skinPreviewPictureBox.Location = new System.Drawing.Point(691, 37);
            this.skinPreviewPictureBox.Name = "skinPreviewPictureBox";
            this.skinPreviewPictureBox.Size = new System.Drawing.Size(138, 281);
            this.skinPreviewPictureBox.TabIndex = 16;
            this.skinPreviewPictureBox.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(748, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 22);
            this.button2.TabIndex = 14;
            this.button2.Text = "Settings";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.ForeColor = System.Drawing.Color.White;
            this.usernameLabel.Location = new System.Drawing.Point(412, 11);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(61, 13);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "Username :";
            this.usernameLabel.Click += new System.EventHandler(this.usernameLabel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.gamePathTextBox);
            this.panel1.Controls.Add(this.gamePathLabel);
            this.panel1.Controls.Add(this.usernameLabel);
            this.panel1.Controls.Add(this.checkforLink);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.openFolderButton);
            this.panel1.Controls.Add(this.setFolderButton);
            this.panel1.Controls.Add(this.logoPictureBox);
            this.panel1.Controls.Add(this.launchButton);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.fullscreenCheckBox);
            this.panel1.Controls.Add(this.playtimeLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 383);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 91);
            this.panel1.TabIndex = 18;
            // 
            // gamePathTextBox
            // 
            this.gamePathTextBox.Location = new System.Drawing.Point(472, 35);
            this.gamePathTextBox.Name = "gamePathTextBox";
            this.gamePathTextBox.Size = new System.Drawing.Size(274, 20);
            this.gamePathTextBox.TabIndex = 16;
            // 
            // gamePathLabel
            // 
            this.gamePathLabel.AutoSize = true;
            this.gamePathLabel.BackColor = System.Drawing.Color.Transparent;
            this.gamePathLabel.ForeColor = System.Drawing.Color.White;
            this.gamePathLabel.Location = new System.Drawing.Point(413, 38);
            this.gamePathLabel.Name = "gamePathLabel";
            this.gamePathLabel.Size = new System.Drawing.Size(60, 13);
            this.gamePathLabel.TabIndex = 7;
            this.gamePathLabel.Text = "GamePath:";
            this.gamePathLabel.Click += new System.EventHandler(this.gamePathLabel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 382);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(844, 2);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SeaShell;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 39);
            this.label1.TabIndex = 20;
            this.label1.Text = "Legacy Console News";
            // 
            // newsFlow
            // 
            this.newsFlow.BackColor = System.Drawing.Color.Transparent;
            this.newsFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.newsFlow.Location = new System.Drawing.Point(0, 51);
            this.newsFlow.Name = "newsFlow";
            this.newsFlow.Size = new System.Drawing.Size(844, 474);
            this.newsFlow.TabIndex = 21;
            this.newsFlow.WrapContents = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(844, 474);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.skinPreviewPictureBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.usernameComboBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.newsFlow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Legacy Console Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPreviewPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.CheckBox fullscreenCheckBox;
        private System.Windows.Forms.ComboBox usernameComboBox;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Button setFolderButton;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.Label playtimeLabel;
        private System.Windows.Forms.LinkLabel checkforLink;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox skinPreviewPictureBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label gamePathLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox gamePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel newsFlow;
    }
}