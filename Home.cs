using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace eHours
{
    public partial class Home : Form
    {
        private string username;
        private string password;
        private string phpSessionId;
        private string nameOfPerson;
        private string nameOfAcademy;
        private string getresp;
        private List<EHourRequest> eHourRequests = new List<EHourRequest>();

        public Home(string username, string password, string phpSessionId, string nameOfPerson, string nameOfAcademy, string getresp)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.phpSessionId = phpSessionId;
            this.nameOfPerson = nameOfPerson;
            this.nameOfAcademy = nameOfAcademy;
            this.getresp = getresp;

            this.Text = $"{nameOfAcademy} Portal for {nameOfPerson} - Made by Neil Patrao";
            this.Load += Home_Load;
            this.Resize += Home_Resize;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            ParseEHourRequests();
            CreateLayout();
        }

        private void Home_Resize(object sender, EventArgs e)
        {
            // Refresh the layout when the form is resized
            CreateLayout();
        }

        private void ParseEHourRequests()
        {
            string pattern = @"<tr class='entry'>.*?<button class='btn' name = 'ehours_request_descr'.*?value = (.*?) .*?>(.*?)</button>.*?<td> (.*?) </td>.*?<td> (.*?) </td>";
            MatchCollection matches = Regex.Matches(getresp, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                string value = match.Groups[1].Value;
                string description = match.Groups[2].Value;
                string hours = match.Groups[3].Value;
                string date = match.Groups[4].Value;

                eHourRequests.Add(new EHourRequest
                {
                    Value = value,
                    Description = description,
                    Hours = hours,
                    Date = date
                });
            }
        }

        private void CreateLayout()
        {
            this.Controls.Clear();

            // Create a panel to hold the content
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            this.Controls.Add(contentPanel);

            // Add title
            Label titleLabel = new Label
            {
                Text = $"{nameOfAcademy} eHours for {nameOfPerson}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Padding = new Padding(0, 20, 0, 20)
            };
            contentPanel.Controls.Add(titleLabel);

            // Add eHour requests
            int yOffset = titleLabel.Bottom + 20;
            foreach (var request in eHourRequests)
            {
                Button requestButton = new Button
                {
                    Text = $"{request.Description}\nHours: {request.Hours}\nDate: {request.Date}",
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Arial", 10),
                    Width = contentPanel.Width - 40,
                    Height = 80,
                    Location = new Point(20, yOffset),
                    Tag = request.Value
                };
                requestButton.Click += RequestButton_Click;
                contentPanel.Controls.Add(requestButton);

                yOffset += requestButton.Height + 10;
            }

            // Optimize background rendering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void RequestButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string value = (string)clickedButton.Tag;
            MessageBox.Show($"Clicked request with value: {value}", "eHour Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Here you can implement the logic to show more details or perform actions related to the clicked eHour request
        }
    }

    public class EHourRequest
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public string Date { get; set; }
    }
}