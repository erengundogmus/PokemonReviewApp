using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace PokemonWinFormsApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            var loginData = new
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text
            };

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsJsonAsync("https://localhost:7013/api/user/login", loginData);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                        if (result != null)
                        {
                            // Token'ı global sınıfa kaydediyoruz
                            UserSession.Token = result.Token;
                            UserSession.Username = result.Username;

                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Hide();
                            MainForm mainForm = new MainForm();
                            mainForm.FormClosed += (s, args) => this.Close();
                            mainForm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
    }
}