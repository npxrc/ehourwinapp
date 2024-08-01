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
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace eHours
{
    public partial class RequestViewer : Form
    {
        private string appDataFolder = "eHours";
        private bool canDelete = false;
        private string id;
        private string phpSessionId;
        private string eventName;
        private CookieContainer _cookieContainer;
        private HttpClientHandler _handler;
        private HttpClient _client;

        private HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

        public RequestViewer(string id, string phpSessionId, string eventName, CookieContainer _cookieContainer, HttpClientHandler _handler, HttpClient _client)
        {
            InitializeComponent();

            this.id = id;
            this.phpSessionId = phpSessionId;
            this.eventName = eventName.Split('\n')[0];
            this._cookieContainer = _cookieContainer;
            this._handler = _handler;
            this._client = _client;

            this.Resize += ResizeFunct;
            ResizeFunct("", EventArgs.Empty);

            _ = PostAsync();

            this.Text = $"Request Viewer - {this.eventName}";
        }

        private void ResizeFunct(object sender, EventArgs e)
        {
            // Event Name Label
            var evtNmX = (this.ClientSize.Width - eventNameLabel.Width) / 2;
            eventNameLabel.Location = new Point(evtNmX, 20);

            // Required Hours Label
            var rqHrX = (this.ClientSize.Width - reqdHrsLabel.Width) / 2;
            reqdHrsLabel.Location = new Point(rqHrX, eventNameLabel.Bottom + 10);

            // Date Submitted Label
            var dtStdX = (this.ClientSize.Width - dateSubtdLabel.Width) / 2;
            dateSubtdLabel.Location = new Point(dtStdX, reqdHrsLabel.Bottom + 5);

            // Description Box
            var descBoxWidth = (int)(this.ClientSize.Width * 0.8);
            var descBoxX = (int)(this.ClientSize.Width * 0.1);
            var descBoxHeight = this.ClientSize.Height - dateSubtdLabel.Bottom - imgLoadingPanel.Height - 40; // 40 for padding
            descriptionBox.Size = new Size(descBoxWidth, descBoxHeight);
            descriptionBox.Location = new Point(descBoxX, dateSubtdLabel.Bottom + 20);

            // Image Loading Panel
            imgLoadingPanel.Width = this.ClientSize.Width;
            imgLoadingPanel.Height = 75; // Fixed height
            imgLoadingPanel.Location = new Point(0, this.ClientSize.Height - imgLoadingPanel.Height);

            // Image Not Supported Label
            var imgNtSptdX = (imgLoadingPanel.Width - imgNotSupported.Width) / 2;
            imgNotSupported.Location = new Point(imgNtSptdX, 10);

            // "But That's Okay" Label
            var btOkayX = (imgLoadingPanel.Width - butThatsOkay.Width) / 2;
            butThatsOkay.Location = new Point(btOkayX, imgNotSupported.Bottom + 5);
        }

        private async Task PostAsync()
        {
            try
            {
                var values = new Dictionary<string, string>
                {
                    { "ehours_request_descr", id }
                };

                var content = new FormUrlEncodedContent(values);

                // Ensure the PHPSESSID cookie is set for the domain
                Uri uri = new Uri("https://academyendorsement.olatheschools.com/");
                _cookieContainer.Add(uri, new Cookie("PHPSESSID", phpSessionId));

                // Set User-Agent if not already set
                if (!_client.DefaultRequestHeaders.Contains("User-Agent"))
                {
                    _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                }

                // Use HTTPS
                var response = await _client.PostAsync("https://academyendorsement.olatheschools.com/Student/eHourDescription.php", content);
                var responseString = await response.Content.ReadAsStringAsync();

                WriteToFile("gettingtheevent.txt", responseString);

                doc.LoadHtml(responseString);
                deleteRequestButton.Visible = false;
                if (doc.DocumentNode.SelectSingleNode("//*[@id='Delete']").InnerHtml.Length>1)
                {
                    deleteRequestButton.Visible = true;
                    Console.WriteLine(doc.DocumentNode.SelectSingleNode("//*[@id='Delete']").InnerHtml);
                    canDelete = true;
                }
                var whiteTextNodes = doc.DocumentNode.SelectNodes("//*[@class='whitetext']");
                if (whiteTextNodes != null && whiteTextNodes.Count >= 2)
                {
                    string reqdHrs = whiteTextNodes[0].InnerText;
                    reqdHrs = HttpUtility.HtmlDecode(reqdHrs);
                    string dateSubtd = whiteTextNodes[1].InnerText;
                    dateSubtd = HttpUtility.HtmlDecode(dateSubtd);
                    string desc = doc.DocumentNode.SelectSingleNode("//textarea[@id='description']")?.InnerText;
                    desc = HttpUtility.HtmlDecode(desc);

                    GetImages(responseString);

                    // Update UI elements with the retrieved data
                    UpdateUIWithData(reqdHrs, dateSubtd, desc);
                }
                else
                {
                    Console.WriteLine("Could not find expected elements in the response.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                WriteToFile("error.txt", ex.Message);
            }
        }

        private void GetImages(string responseString)
        {
            var imgs = doc.DocumentNode.SelectNodes("//img");
            //todo: actually write this code
        }

        private void UpdateUIWithData(string reqdHrs, string dateSubtd, string desc)
        {
            // Update your UI elements here, for example:
            eventNameLabel.Text = this.eventName;
            reqdHrsLabel.Text = reqdHrs;
            dateSubtdLabel.Text = dateSubtd;
            descriptionBox.Text = desc;
            ResizeFunct("", EventArgs.Empty);
        }
        private string ReadFromFile(string filename)
        {
            try
            {
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                string dataPath = Path.Combine(localAppDataPath, appDataFolder);

                string filePath = Path.Combine(dataPath, filename);

                if (File.Exists(filePath))
                {
                    string textFromFile = File.ReadAllText(filePath);
                    return textFromFile;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private void WriteToFile(string filename, string toWrite)
        {
            try
            {
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                string dataPath = Path.Combine(localAppDataPath, appDataFolder);

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                string filePath = Path.Combine(dataPath, filename);

                string textToWrite = toWrite;
                File.WriteAllText(filePath, textToWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void deleteReq(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Are you sure you want to delete this?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (delete == DialogResult.Yes)
            {
                try
                {
                    var values = new Dictionary<string, string>
                    {
                        { "del", id }
                    };

                    var content = new FormUrlEncodedContent(values);

                    // Ensure the PHPSESSID cookie is set for the domain
                    Uri uri = new Uri("https://academyendorsement.olatheschools.com/");
                    _cookieContainer.Add(uri, new Cookie("PHPSESSID", phpSessionId));

                    // Set User-Agent if not already set
                    if (!_client.DefaultRequestHeaders.Contains("User-Agent"))
                    {
                        _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                    }

                    // Use HTTPS
                    var response = await _client.PostAsync("https://academyendorsement.olatheschools.com/deleteRequest.php", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    WriteToFile("delreq.txt", responseString);

                    this.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    WriteToFile("error.txt", ex.Message);
                }
            }
        }
    }
}
