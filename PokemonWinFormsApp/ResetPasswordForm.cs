using PokemonWinFormsApp.InputDtos;

namespace PokemonWinFormsApp.Auth
{
    public partial class ResetPasswordForm : Form
    {
        private readonly IApiService _apiService;

        public ResetPasswordForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void buttonReset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textUsername.Text) || string.IsNullOrWhiteSpace(textNewPassword.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resetDto = new ResetPasswordDto
            {
                Username = textUsername.Text.Trim(),
                NewPassword = textNewPassword.Text.Trim()
            };

            try
            {
                bool isSuccess = await _apiService.CreateAsync("user/reset-password", resetDto);

                if (isSuccess)
                {
                    MessageBox.Show("Password successfully updated! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update password. Please check your username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}