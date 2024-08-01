using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Web;
using System.IO;

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
        private CookieContainer _cookieContainer;
        private HttpClientHandler _handler;
        private HttpClient _client;

        private HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        private List<EHourRequest> eHourRequests = new List<EHourRequest>();

        public Home(string username, string password, string phpSessionId, string nameOfPerson, string nameOfAcademy, string getresp, CookieContainer _cookieContainer, HttpClientHandler _handler, HttpClient _client)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.phpSessionId = phpSessionId;
            this.nameOfPerson = nameOfPerson;
            this.nameOfAcademy = nameOfAcademy;
            this.getresp = getresp;

            this._cookieContainer = _cookieContainer;
            this._handler = _handler;
            this._client = _client;

            doc.LoadHtml(getresp);

            this.Load += Home_Load;
            this.Resize += Home_Resize;

            studentName.Text = nameOfPerson;
            studentAcademy.Text = nameOfAcademy;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            ParseEHourRequests();
            CreateLayout();
            Home_Resize("", EventArgs.Empty);
        }

        private void Home_Resize(object sender, EventArgs e)
        {
            homeMenuPanel.Location = new Point(0, 100);
            homeMenuPanel.Width = this.ClientSize.Width;
            homeMenuPanel.Height = this.ClientSize.Height;
            foreach (var button in requestButtons)
            {
                button.Width = homeMenuPanel.ClientSize.Width - 40;
            }

            var stdNameX = ((this.ClientSize.Width / 2) - studentName.Width) / 2;
            studentName.Location = new Point(stdNameX, 10);

            var stdAcadX = ((this.ClientSize.Width / 2) - studentAcademy.Width) / 2;
            studentAcademy.Location = new Point(stdAcadX, (10+studentName.Height));

            var divX = (this.ClientSize.Width - dividerBlack.Width) / 2;
            dividerBlack.Location = new Point(divX, 11);

            var currEhrsX = (((this.ClientSize.Width / 2) - eHourCount.Width) / 2)+(this.ClientSize.Width/2);
            var currEhrsY = (100 - eHourCount.Height) / 2;
            eHourCount.Location = new Point(currEhrsX, currEhrsY);
        }
        private string RemoveExcessLineBreaks(string text)
        {
            // Normalize line breaks by ensuring a consistent line break character
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");

            // Split the text into lines
            string[] lines = text.Split('\n');

            // Remove the first two and last two lines
            if (lines.Length <= 4)
            {
                // If there are four or fewer lines, return an empty string or handle as needed
                return string.Empty;
            }

            // Take lines from index 2 to the length minus 2
            string result = string.Join("\n", lines.Skip(2).Take(lines.Length - 4));

            return result;
        }

        private void ParseEHourRequests()
        {
            // Use XPath to select all <tr> elements with class 'entry'
            var rows = doc.DocumentNode.SelectNodes("//tr[@class='entry']");

            if (rows != null)
            {
                foreach (var row in rows)
                {
                    // Extract data from the <button> element within the <tr>
                    var buttonNode = row.SelectSingleNode(".//button[@name='ehours_request_descr']");
                    var value = buttonNode.GetAttributeValue("value", string.Empty);
                    var description = buttonNode.InnerText.Trim();
                    description = HttpUtility.HtmlDecode(description);

                    // Extract the values from <td> elements within the <tr>
                    var tdNodes = row.SelectNodes(".//td");
                    if (tdNodes != null && tdNodes.Count >= 3)
                    {
                        var hours = tdNodes[1].InnerText.Trim();
                        var date = tdNodes[2].InnerText.Trim();

                        eHourRequests.Add(new EHourRequest
                        {
                            Value = value,
                            Description = description,
                            Hours = hours,
                            Date = date
                        });
                    }
                }
            }
        }

        private List<Button> requestButtons = new List<Button>();  // Store buttons in a list

        private void CreateLayout()
        {
            homeMenuPanel.Controls.Clear();
            homeMenuPanel.AutoScroll = true;

            // Add or update the eHourCount label
            var node = doc.DocumentNode.SelectSingleNode("//*[@id='HourCount']");
            if (node != null)
            {
                eHourCount.Text = RemoveExcessLineBreaks(node.InnerText);
            }

            // Add or update the title label
            Label titleLabel = new Label
            {
                Text = $"{nameOfAcademy} eHours for {nameOfPerson}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Padding = new Padding(0, 20, 0, 20)
            };
            homeMenuPanel.Controls.Add(titleLabel);

            // Adjust existing buttons or create new ones
            int yOffset = titleLabel.Height + 20;
            for (int i = 0; i < eHourRequests.Count; i++)
            {
                Button requestButton;
                if (i < requestButtons.Count)
                {
                    // Update existing button
                    requestButton = requestButtons[i];
                }
                else
                {
                    // Create new button if needed
                    requestButton = new Button
                    {
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Arial", 10),
                        Width = homeMenuPanel.ClientSize.Width - 40,
                        Height = 80,
                        Location = new Point(20, yOffset),
                        Tag = eHourRequests[i].Value,
                        BackColor = Color.FromArgb(60, 60, 60),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    requestButton.FlatAppearance.BorderColor = Color.Gray;
                    requestButton.Click += RequestButton_Click;
                    homeMenuPanel.Controls.Add(requestButton);
                    requestButtons.Add(requestButton);  // Add to the list
                }

                // Update the button's properties
                requestButton.Text = $"{eHourRequests[i].Description}\nHours: {eHourRequests[i].Hours}\nDate: {eHourRequests[i].Date}";
                requestButton.Location = new Point(20, yOffset);

                yOffset += requestButton.Height + 10;
            }

            // Remove excess buttons
            while (requestButtons.Count > eHourRequests.Count)
            {
                Button buttonToRemove = requestButtons.Last();
                requestButtons.Remove(buttonToRemove);
                homeMenuPanel.Controls.Remove(buttonToRemove);
                buttonToRemove.Dispose();  // Clean up
            }

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void RequestButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string value = (string)clickedButton.Tag;
            RequestViewer requestViewer = new RequestViewer(value, phpSessionId, clickedButton.Text, _cookieContainer, _handler, _client);
            requestViewer.Show();
            requestViewer.Location = new Point(this.Location.X, this.Location.Y);
            requestViewer.Size = new Size(750, 500);
        }
    }

    public class EHourRequest
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public string Date { get; set; }
    }

    //  TODO add support for uploading (probably make a new .CS file)
    //  POSTs to https://academyendorsement.olatheschools.com/Student/makeRequest.php
    //  5 elements:
    //      - title box (name="title" in POST)
    //      - activity date (name="activityDate" in POST)
    //      - number of hours req'd (name="hours")
    //      - description (name="description")
    //      - images (name="img[]" ? idk)
    //          - allow multiple images
}