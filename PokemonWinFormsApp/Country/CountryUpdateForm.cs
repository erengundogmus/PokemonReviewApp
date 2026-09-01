using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryUpdateForm : Form
    {
        private readonly IApiService _apiService;
        private int _countryId;

        public CountryUpdateForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public async Task LoadCountryForUpdateAsync(int countryId)
        {
            _countryId = countryId;
            try
            {
                var country = await _apiService.GetByIdAsync<CountryOutputDto>("country", _countryId);
                if (country != null)
                {
                    textName.Text = country.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedCountry = new CountryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                bool isSuccess = await _apiService.UpdateAsync("country", _countryId, updatedCountry);

                if (isSuccess)
                {
                    MessageBox.Show("Country successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update country.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}