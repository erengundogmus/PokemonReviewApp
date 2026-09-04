using PokemonReviewApp.InputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryCreateForm : Form
    {
        private readonly IApiService _apiService;

        public CountryCreateForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void CountryCreate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CountryCreate"))
            {
                MessageBox.Show("You do not have permission to create a country.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCountry = new CountryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                bool isSuccess = await _apiService.CreateAsync("country", newCountry);

                if (isSuccess)
                {
                    MessageBox.Show("Country successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create country.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}