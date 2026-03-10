namespace LegacyConsoleLauncher
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button saveButton;
            this.checkGameUpdatesCheckBox = new System.Windows.Forms.CheckBox();
            this.checkLauncherUpdatesCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.launchArgsTextBox = new System.Windows.Forms.TextBox();
            this.openDataFolderButton = new System.Windows.Forms.Button();
            this.openSkinsFolderButton = new System.Windows.Forms.Button();
            this.resetLauncherSettingsButton = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.githubLinkLabel = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            saveButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(171, 218);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 18;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // checkGameUpdatesCheckBox
            // 
            this.checkGameUpdatesCheckBox.AutoSize = true;
            this.checkGameUpdatesCheckBox.Location = new System.Drawing.Point(6, 19);
            this.checkGameUpdatesCheckBox.Name = "checkGameUpdatesCheckBox";
            this.checkGameUpdatesCheckBox.Size = new System.Drawing.Size(192, 17);
            this.checkGameUpdatesCheckBox.TabIndex = 1;
            this.checkGameUpdatesCheckBox.Text = "Check for game updates on startup";
            this.checkGameUpdatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkLauncherUpdatesCheckBox
            // 
            this.checkLauncherUpdatesCheckBox.AutoSize = true;
            this.checkLauncherUpdatesCheckBox.Location = new System.Drawing.Point(6, 36);
            this.checkLauncherUpdatesCheckBox.Name = "checkLauncherUpdatesCheckBox";
            this.checkLauncherUpdatesCheckBox.Size = new System.Drawing.Size(207, 17);
            this.checkLauncherUpdatesCheckBox.TabIndex = 2;
            this.checkLauncherUpdatesCheckBox.Text = "Check for launcher updates on startup";
            this.checkLauncherUpdatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Additional launch arguments:";
            // 
            // launchArgsTextBox
            // 
            this.launchArgsTextBox.Location = new System.Drawing.Point(9, 32);
            this.launchArgsTextBox.Name = "launchArgsTextBox";
            this.launchArgsTextBox.Size = new System.Drawing.Size(232, 20);
            this.launchArgsTextBox.TabIndex = 6;
            // 
            // openDataFolderButton
            // 
            this.openDataFolderButton.Location = new System.Drawing.Point(6, 13);
            this.openDataFolderButton.Name = "openDataFolderButton";
            this.openDataFolderButton.Size = new System.Drawing.Size(140, 23);
            this.openDataFolderButton.TabIndex = 8;
            this.openDataFolderButton.Text = "Open launcher data folder";
            this.openDataFolderButton.UseVisualStyleBackColor = true;
            this.openDataFolderButton.Click += new System.EventHandler(this.openDataFolderButton_Click);
            // 
            // openSkinsFolderButton
            // 
            this.openSkinsFolderButton.Location = new System.Drawing.Point(6, 43);
            this.openSkinsFolderButton.Name = "openSkinsFolderButton";
            this.openSkinsFolderButton.Size = new System.Drawing.Size(140, 23);
            this.openSkinsFolderButton.TabIndex = 9;
            this.openSkinsFolderButton.Text = "Open skins folder";
            this.openSkinsFolderButton.UseVisualStyleBackColor = true;
            this.openSkinsFolderButton.Click += new System.EventHandler(this.openSkinsFolderButton_Click);
            // 
            // resetLauncherSettingsButton
            // 
            this.resetLauncherSettingsButton.Location = new System.Drawing.Point(6, 73);
            this.resetLauncherSettingsButton.Name = "resetLauncherSettingsButton";
            this.resetLauncherSettingsButton.Size = new System.Drawing.Size(140, 23);
            this.resetLauncherSettingsButton.TabIndex = 10;
            this.resetLauncherSettingsButton.Text = "Reset launcher settings";
            this.resetLauncherSettingsButton.UseVisualStyleBackColor = true;
            this.resetLauncherSettingsButton.Click += new System.EventHandler(this.resetLauncherSettingsButton_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(6, 21);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(164, 13);
            this.versionLabel.TabIndex = 12;
            this.versionLabel.Text = "Legacy Console Launcher v1.3.0";
            // 
            // githubLinkLabel
            // 
            this.githubLinkLabel.AutoSize = true;
            this.githubLinkLabel.Location = new System.Drawing.Point(7, 38);
            this.githubLinkLabel.Name = "githubLinkLabel";
            this.githubLinkLabel.Size = new System.Drawing.Size(220, 13);
            this.githubLinkLabel.TabIndex = 13;
            this.githubLinkLabel.TabStop = true;
            this.githubLinkLabel.Text = "github.com/OxyZin/LegacyConsoleLauncher";
            this.githubLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLinkLabel_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkGameUpdatesCheckBox);
            this.groupBox1.Controls.Add(this.checkLauncherUpdatesCheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 57);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.launchArgsTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 60);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Advanced";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.resetLauncherSettingsButton);
            this.groupBox3.Controls.Add(this.openDataFolderButton);
            this.groupBox3.Controls.Add(this.openSkinsFolderButton);
            this.groupBox3.Location = new System.Drawing.Point(12, 141);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(153, 100);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Maintenance";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.versionLabel);
            this.groupBox4.Controls.Add(this.githubLinkLabel);
            this.groupBox4.Location = new System.Drawing.Point(12, 247);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 59);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "About";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 310);
            this.Controls.Add(saveButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form3";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox checkGameUpdatesCheckBox;
        private System.Windows.Forms.CheckBox checkLauncherUpdatesCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox launchArgsTextBox;
        private System.Windows.Forms.Button openDataFolderButton;
        private System.Windows.Forms.Button openSkinsFolderButton;
        private System.Windows.Forms.Button resetLauncherSettingsButton;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.LinkLabel githubLinkLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}