//App.cs

using System.Net;
using System.IO;
using System.Windows.Forms;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace eHours
{
    public partial class App : Form
    {
        private string appDataFolder = "eHours";
        private string uNamePlaceholderText = "Enter your username";
        private string uPassPlaceholderText = "Enter your password";
        private Color placeholderColor = Color.Gray;
        private Color textColor = Color.Black;
        private string phpSessionId;
        private string nameOfPerson;
        private string nameOfAcademy;
        private string username;
        private string password;

        public App()
        {
            InitializeComponent();
            InitializePlaceholders();
            this.Load += CenterMenuItems;
            this.Resize += OnFormResize;
            OnFormResize(this, EventArgs.Empty);
            menuStudentLogin.Cursor = Cursors.Arrow;
            menuExitApp.Cursor = Cursors.Arrow;

            try
            {
                var (username, password) = CredentialManager.ReadCredential("eHours");
                if (!string.IsNullOrEmpty(username))
                {
                    uName.Text = username;
                    uName.ForeColor = textColor;
                }
                if (!string.IsNullOrEmpty(password))
                {
                    uPass.Text = password;
                    uPass.ForeColor = textColor;
                    uPass.UseSystemPasswordChar = true;
                }
            }
            catch (Exception ex)
            {
                // Handle the case where no credentials are found
                Console.WriteLine($"No stored credentials found: {ex.Message}");
            }
        }

        private void InitializePlaceholders()
        {
            // Set up uName placeholder
            uName.ForeColor = placeholderColor;
            uName.Text = uNamePlaceholderText;
            uName.Enter += UName_Enter;
            uName.Leave += UName_Leave;

            // Set up uPass placeholder
            uPass.ForeColor = placeholderColor;
            uPass.Text = uPassPlaceholderText;
            uPass.Enter += UPass_Enter;
            uPass.Leave += UPass_Leave;
            uPass.UseSystemPasswordChar = false; // Show the placeholder text initially
        }
        private void UName_Enter(object sender, EventArgs e)
        {
            if (uName.ForeColor == placeholderColor)
            {
                uName.Text = "";
                uName.ForeColor = textColor;
            }
        }
        private void UName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(uName.Text))
            {
                uName.ForeColor = placeholderColor;
                uName.Text = uNamePlaceholderText;
            }
        }
        private void UPass_Enter(object sender, EventArgs e)
        {
            if (uPass.ForeColor == placeholderColor)
            {
                uPass.Text = "";
                uPass.ForeColor = textColor;
                uPass.UseSystemPasswordChar = true;
            }
        }
        private void UPass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(uPass.Text))
            {
                uPass.UseSystemPasswordChar = false;
                uPass.ForeColor = placeholderColor;
                uPass.Text = uPassPlaceholderText;
            }
        }

        private static readonly CookieContainer _cookieContainer = new CookieContainer();
        private static readonly HttpClientHandler _handler = new HttpClientHandler
        {
            CookieContainer = _cookieContainer,
            AllowAutoRedirect = true
        };
        private static readonly HttpClient _client = new HttpClient(_handler);

        private async Task<string> Post()
        {
            var values = new Dictionary<string, string>
            {
                { "uName", username },
                { "uPass", password }
            };

            var content = new FormUrlEncodedContent(values);

            // Set a User-Agent
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            // Use HTTPS
            var response = await _client.PostAsync("https://academyendorsement.olatheschools.com/loginuserstudent.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Access the PHPSESSID cookie
            Uri uri = new Uri("https://academyendorsement.olatheschools.com/");
            var cookies = _cookieContainer.GetCookies(uri);
            phpSessionId = cookies["PHPSESSID"]?.Value;

            return responseString;
        }
        private async Task<string> Get(string url)
        {
            if (string.IsNullOrEmpty(phpSessionId))
            {
                MessageBox.Show("PHPSESSID cookie is not set.");
                return "$$FAIL$$";
            }

            // Ensure the PHPSESSID cookie is set for the domain
            Uri uri = new Uri(url);
            _cookieContainer.Add(uri, new Cookie("PHPSESSID", phpSessionId));

            var response = await _client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
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
        private void OnFormResize(object sender, EventArgs e)
        {
            // Center the PictureBox on form resize
            CenterMenuItems("", EventArgs.Empty);
            mainMenuPanel.Width = this.Width;
            mainMenuPanel.Height = this.Height;
        }
        private void CenterMenuItems(object sender, EventArgs e)
        {
            // Calculate the top-left position to center the PictureBox
            menuLogo.Width = (this.Width / 3);
            menuLogo.Height = (this.Height / 3);
            int logoX = (this.ClientSize.Width - menuLogo.Width) / 2;
            int contX = (this.ClientSize.Width - menuButtonCont.Width) / 2;
            int logoY = (this.ClientSize.Height - menuLogo.Height) / 8;
            int contY = (this.ClientSize.Height - menuButtonCont.Height) / (1 + 15 / 17);

            // Set the location of the PictureBox
            menuLogo.Location = new System.Drawing.Point(logoX, logoY);
            menuButtonCont.Location = new System.Drawing.Point(contX, contY);
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private async void menuStudentLogin_Click(object sender, EventArgs evtarg)
        {
            if (uName.Text == uNamePlaceholderText || uPass.Text == uPassPlaceholderText)
            {
                MessageBox.Show("Enter your district username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            username = uName.Text;
            password = uPass.Text;
            CredentialManager.WriteCredential("eHours", username, password);
            string resp = await Post();
            resp = resp.Replace("\n", "");
            if (resp.Contains("<h2>Welcome to your"))
            {
                nameOfAcademy = resp.Split(new string[] { "<h2>Welcome to your " }, StringSplitOptions.None)[1].Split(new string[] { " Endorsement" }, StringSplitOptions.None)[0];
                nameOfPerson = resp.Split(new string[] { "Tracking, " }, StringSplitOptions.None)[1].Split(new string[] { "</h2>" }, StringSplitOptions.None)[0];
                Console.WriteLine(nameOfPerson);
                Console.WriteLine(nameOfAcademy);

                string getresp = await Get("https://academyendorsement.olatheschools.com/Student/studentEHours.php");

                // Create and show the new form, passing the necessary parameters and reference to this form
                Home home = new Home(username, password, phpSessionId, nameOfPerson, nameOfAcademy, getresp, _cookieContainer, _handler, _client);
                this.Hide();
                home.Show();
                home.Location = new Point(this.Location.X, this.Location.Y);
                home.Size = new Size(this.Size.Width, this.Size.Height);
                home.FormClosing += (s, e) => { this.Close(); };
            }
            else
            {
                MessageBox.Show("Incorrect username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openGithub(object sender, EventArgs e)
        {
            string url = "https://github.com/npxrc/ehourwinapp";
            System.Diagnostics.Process.Start(url);
        }
    }
}