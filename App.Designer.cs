using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace eHours
{
    partial class App
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.mainMenuPanel = new System.Windows.Forms.Panel();
            this.menuButtonCont = new System.Windows.Forms.Panel();
            this.githubOpen = new System.Windows.Forms.Label();
            this.uPass = new System.Windows.Forms.TextBox();
            this.uName = new System.Windows.Forms.TextBox();
            this.menuExitApp = new System.Windows.Forms.Button();
            this.menuStudentLogin = new System.Windows.Forms.Button();
            this.menuLogo = new System.Windows.Forms.PictureBox();
            this.mainMenuPanel.SuspendLayout();
            this.menuButtonCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuPanel
            // 
            this.mainMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainMenuPanel.Controls.Add(this.menuButtonCont);
            this.mainMenuPanel.Controls.Add(this.menuLogo);
            this.mainMenuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.mainMenuPanel.Name = "mainMenuPanel";
            this.mainMenuPanel.Size = new System.Drawing.Size(750, 500);
            this.mainMenuPanel.TabIndex = 0;
            // 
            // menuButtonCont
            // 
            this.menuButtonCont.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonCont.Controls.Add(this.githubOpen);
            this.menuButtonCont.Controls.Add(this.uPass);
            this.menuButtonCont.Controls.Add(this.uName);
            this.menuButtonCont.Controls.Add(this.menuExitApp);
            this.menuButtonCont.Controls.Add(this.menuStudentLogin);
            this.menuButtonCont.Location = new System.Drawing.Point(211, 273);
            this.menuButtonCont.Name = "menuButtonCont";
            this.menuButtonCont.Size = new System.Drawing.Size(320, 150);
            this.menuButtonCont.TabIndex = 3;
            // 
            // githubOpen
            // 
            this.githubOpen.AutoSize = true;
            this.githubOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.githubOpen.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.githubOpen.Location = new System.Drawing.Point(78, 0);
            this.githubOpen.Name = "githubOpen";
            this.githubOpen.Size = new System.Drawing.Size(147, 42);
            this.githubOpen.TabIndex = 4;
            this.githubOpen.Text = "This is NOT an official app\r\nSource code on GitHub\r\nnpxrc/ehourwinapp";
            this.githubOpen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.githubOpen.Click += new System.EventHandler(this.openGithub);
            // 
            // uPass
            // 
            this.uPass.Font = new System.Drawing.Font("Consolas", 9F);
            this.uPass.Location = new System.Drawing.Point(162, 55);
            this.uPass.Name = "uPass";
            this.uPass.Size = new System.Drawing.Size(154, 22);
            this.uPass.TabIndex = 3;
            this.uPass.UseSystemPasswordChar = true;
            // 
            // uName
            // 
            this.uName.Font = new System.Drawing.Font("Consolas", 9F);
            this.uName.Location = new System.Drawing.Point(4, 55);
            this.uName.Name = "uName";
            this.uName.Size = new System.Drawing.Size(154, 22);
            this.uName.TabIndex = 2;
            this.uName.Text = "test";
            // 
            // menuExitApp
            // 
            this.menuExitApp.Font = new System.Drawing.Font("Rubik", 9.75F);
            this.menuExitApp.Location = new System.Drawing.Point(161, 81);
            this.menuExitApp.Name = "menuExitApp";
            this.menuExitApp.Size = new System.Drawing.Size(155, 40);
            this.menuExitApp.TabIndex = 1;
            this.menuExitApp.Text = "Exit";
            this.menuExitApp.UseVisualStyleBackColor = true;
            this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
            // 
            // menuStudentLogin
            // 
            this.menuStudentLogin.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStudentLogin.Location = new System.Drawing.Point(3, 81);
            this.menuStudentLogin.Name = "menuStudentLogin";
            this.menuStudentLogin.Size = new System.Drawing.Size(155, 40);
            this.menuStudentLogin.TabIndex = 0;
            this.menuStudentLogin.Text = "Login";
            this.menuStudentLogin.UseVisualStyleBackColor = true;
            this.menuStudentLogin.Click += new System.EventHandler(this.menuStudentLogin_Click);
            // 
            // menuLogo
            // 
            this.menuLogo.ImageLocation = "https://academyendorsement.olatheschools.com/images/21century_acad_logo.png";
            this.menuLogo.Location = new System.Drawing.Point(211, 67);
            this.menuLogo.Name = "menuLogo";
            this.menuLogo.Size = new System.Drawing.Size(320, 200);
            this.menuLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.menuLogo.TabIndex = 2;
            this.menuLogo.TabStop = false;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(113)))), ((int)(((byte)(127)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.mainMenuPanel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "App";
            this.Text = "eHour Portal - Made by Neil Patrao";
            this.mainMenuPanel.ResumeLayout(false);
            this.menuButtonCont.ResumeLayout(false);
            this.menuButtonCont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel mainMenuPanel;
        private Panel menuButtonCont;
        private Button menuExitApp;
        private Button menuStudentLogin;
        private PictureBox menuLogo;
        private TextBox uPass;
        private TextBox uName;
        private Label githubOpen;
    }
}