using Autofac;
using System;
using System.Windows.Forms;

namespace PokemonWinFormsApp
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;
        public LoginForm(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            var loginData = new
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text
            };

            try
            {
                //istek doğrudan servise devredildi. 
                var result = await _authService.LoginAsync("user/login", loginData);

                if (result != null)
                {
                    //tokenı global sınıfa kaydediyoruz
                    UserSession.Token = result.Token;
                    UserSession.Username = result.Username;

                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();

                    var mainForm = Program.Container.Resolve<MainForm>();
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
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
