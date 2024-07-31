namespace eHours
{
    partial class RequestViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestViewer));
            this.eventNameLabel = new System.Windows.Forms.Label();
            this.reqdHrsLabel = new System.Windows.Forms.Label();
            this.dateSubtdLabel = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.imgLoadingPanel = new System.Windows.Forms.Panel();
            this.butThatsOkay = new System.Windows.Forms.Label();
            this.imgNotSupported = new System.Windows.Forms.Label();
            this.imgLoadingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // eventNameLabel
            // 
            this.eventNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.eventNameLabel.AutoSize = true;
            this.eventNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.eventNameLabel.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventNameLabel.Location = new System.Drawing.Point(331, 20);
            this.eventNameLabel.Name = "eventNameLabel";
            this.eventNameLabel.Size = new System.Drawing.Size(88, 29);
            this.eventNameLabel.TabIndex = 0;
            this.eventNameLabel.Text = "Loading";
            // 
            // reqdHrsLabel
            // 
            this.reqdHrsLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.reqdHrsLabel.AutoSize = true;
            this.reqdHrsLabel.BackColor = System.Drawing.Color.Transparent;
            this.reqdHrsLabel.Font = new System.Drawing.Font("Rubik", 10F);
            this.reqdHrsLabel.Location = new System.Drawing.Point(352, 55);
            this.reqdHrsLabel.Name = "reqdHrsLabel";
            this.reqdHrsLabel.Size = new System.Drawing.Size(31, 22);
            this.reqdHrsLabel.TabIndex = 1;
            this.reqdHrsLabel.Text = "---";
            // 
            // dateSubtdLabel
            // 
            this.dateSubtdLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateSubtdLabel.AutoSize = true;
            this.dateSubtdLabel.BackColor = System.Drawing.Color.Transparent;
            this.dateSubtdLabel.Font = new System.Drawing.Font("Rubik", 10F);
            this.dateSubtdLabel.Location = new System.Drawing.Point(352, 77);
            this.dateSubtdLabel.Name = "dateSubtdLabel";
            this.dateSubtdLabel.Size = new System.Drawing.Size(31, 22);
            this.dateSubtdLabel.TabIndex = 3;
            this.dateSubtdLabel.Text = "---";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.BackColor = System.Drawing.Color.Gray;
            this.descriptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.descriptionBox.Font = new System.Drawing.Font("Rubik Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionBox.Location = new System.Drawing.Point(100, 100);
            this.descriptionBox.Margin = new System.Windows.Forms.Padding(10);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionBox.Size = new System.Drawing.Size(550, 300);
            this.descriptionBox.TabIndex = 4;
            this.descriptionBox.Text = "if you reading, this chill out, still making a request to the ehours portal. if t" +
    "his text doesn\'t update soon, check your internet connection";
            // 
            // imgLoadingPanel
            // 
            this.imgLoadingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLoadingPanel.BackColor = System.Drawing.Color.Transparent;
            this.imgLoadingPanel.Controls.Add(this.butThatsOkay);
            this.imgLoadingPanel.Controls.Add(this.imgNotSupported);
            this.imgLoadingPanel.Location = new System.Drawing.Point(0, 425);
            this.imgLoadingPanel.Name = "imgLoadingPanel";
            this.imgLoadingPanel.Size = new System.Drawing.Size(750, 75);
            this.imgLoadingPanel.TabIndex = 2;
            // 
            // butThatsOkay
            // 
            this.butThatsOkay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.butThatsOkay.AutoSize = true;
            this.butThatsOkay.BackColor = System.Drawing.Color.Transparent;
            this.butThatsOkay.Font = new System.Drawing.Font("Rubik SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butThatsOkay.Location = new System.Drawing.Point(189, 45);
            this.butThatsOkay.Name = "butThatsOkay";
            this.butThatsOkay.Size = new System.Drawing.Size(372, 20);
            this.butThatsOkay.TabIndex = 4;
            this.butThatsOkay.Text = "but that\'s okay since you remember what you uploaded!";
            // 
            // imgNotSupported
            // 
            this.imgNotSupported.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgNotSupported.AutoSize = true;
            this.imgNotSupported.BackColor = System.Drawing.Color.Transparent;
            this.imgNotSupported.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgNotSupported.Location = new System.Drawing.Point(220, 10);
            this.imgNotSupported.Name = "imgNotSupported";
            this.imgNotSupported.Size = new System.Drawing.Size(310, 29);
            this.imgNotSupported.TabIndex = 3;
            this.imgNotSupported.Text = "Image Loading is not supported";
            // 
            // RequestViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.dateSubtdLabel);
            this.Controls.Add(this.imgLoadingPanel);
            this.Controls.Add(this.reqdHrsLabel);
            this.Controls.Add(this.eventNameLabel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(766, 539);
            this.Name = "RequestViewer";
            this.Text = "eHour Portal - Made by Neil Patrao";
            this.imgLoadingPanel.ResumeLayout(false);
            this.imgLoadingPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label eventNameLabel;
        private System.Windows.Forms.Label reqdHrsLabel;
        private System.Windows.Forms.Label dateSubtdLabel;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Panel imgLoadingPanel;
        private System.Windows.Forms.Label butThatsOkay;
        private System.Windows.Forms.Label imgNotSupported;
    }
}