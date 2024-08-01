namespace eHours
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.homeMenuPanel = new System.Windows.Forms.Panel();
            this.dividerBlack = new System.Windows.Forms.Panel();
            this.studentName = new System.Windows.Forms.Label();
            this.studentAcademy = new System.Windows.Forms.Label();
            this.eHourCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // homeMenuPanel
            // 
            this.homeMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.homeMenuPanel.Location = new System.Drawing.Point(0, 100);
            this.homeMenuPanel.Name = "homeMenuPanel";
            this.homeMenuPanel.Size = new System.Drawing.Size(750, 400);
            this.homeMenuPanel.TabIndex = 1;
            // 
            // dividerBlack
            // 
            this.dividerBlack.BackColor = System.Drawing.Color.Black;
            this.dividerBlack.Location = new System.Drawing.Point(374, 11);
            this.dividerBlack.Name = "dividerBlack";
            this.dividerBlack.Size = new System.Drawing.Size(2, 78);
            this.dividerBlack.TabIndex = 2;
            // 
            // studentName
            // 
            this.studentName.AutoSize = true;
            this.studentName.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Bold);
            this.studentName.Location = new System.Drawing.Point(34, 10);
            this.studentName.MaximumSize = new System.Drawing.Size(325, 75);
            this.studentName.Name = "studentName";
            this.studentName.Size = new System.Drawing.Size(300, 72);
            this.studentName.TabIndex = 3;
            this.studentName.Text = "your name would go here if you had a decent computer\r\ni guess its too slow";
            this.studentName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // studentAcademy
            // 
            this.studentAcademy.AutoSize = true;
            this.studentAcademy.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Bold);
            this.studentAcademy.Location = new System.Drawing.Point(49, 70);
            this.studentAcademy.MaximumSize = new System.Drawing.Size(325, 75);
            this.studentAcademy.Name = "studentAcademy";
            this.studentAcademy.Size = new System.Drawing.Size(283, 24);
            this.studentAcademy.TabIndex = 4;
            this.studentAcademy.Text = "your academy name would go here";
            this.studentAcademy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // eHourCount
            // 
            this.eHourCount.AutoSize = true;
            this.eHourCount.Font = new System.Drawing.Font("Rubik SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eHourCount.Location = new System.Drawing.Point(458, 10);
            this.eHourCount.MaximumSize = new System.Drawing.Size(325, 75);
            this.eHourCount.Name = "eHourCount";
            this.eHourCount.Size = new System.Drawing.Size(210, 20);
            this.eHourCount.TabIndex = 5;
            this.eHourCount.Text = "your # of ehours would go here";
            this.eHourCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(113)))), ((int)(((byte)(127)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.eHourCount);
            this.Controls.Add(this.studentAcademy);
            this.Controls.Add(this.studentName);
            this.Controls.Add(this.dividerBlack);
            this.Controls.Add(this.homeMenuPanel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Home";
            this.Text = "eHour Portal - Made by Neil Patrao";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel homeMenuPanel;
        private System.Windows.Forms.Panel dividerBlack;
        private System.Windows.Forms.Label studentName;
        private System.Windows.Forms.Label studentAcademy;
        private System.Windows.Forms.Label eHourCount;
    }
}