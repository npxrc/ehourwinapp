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
        }

        private void Home_Load(object sender, EventArgs e)
        {
            ParseEHourRequests();
            CreateLayout();
        }

        private void Home_Resize(object sender, EventArgs e)
        {
            homeMenuPanel.Width = this.ClientSize.Width;
            homeMenuPanel.Height = this.ClientSize.Height;
            foreach (var button in requestButtons)
            {
                button.Width = homeMenuPanel.ClientSize.Width - 40;
            }
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
                Label eHourCount = new Label
                {
                    Text = node.InnerText.Replace("\t", "").Trim(),
                    AutoSize = true,
                    Location = new Point(10, 10),
                    Font = new Font("Arial", 12),
                    ForeColor = Color.White
                };
                homeMenuPanel.Controls.Add(eHourCount);
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
            requestViewer.Size = new Size(this.Size.Width, this.Size.Height);
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