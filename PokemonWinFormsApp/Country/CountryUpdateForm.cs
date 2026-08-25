using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryUpdateForm : Form
    {
        private readonly int _countryId;
        private readonly string apiUrl = "https://localhost:7013/api/country/";
        private readonly HttpClient client = new HttpClient();

        public CountryUpdateForm(int countryId)
        {
            InitializeComponent();
            _countryId = countryId;

            //form açıldığı an mevcut bilgileri form kutularına doldurur
            _ = LoadCountryDataAsync();
        }

        private async Task LoadCountryDataAsync()
        {
            try
            {
                var country = await client.GetFromJsonAsync<CountryOutputDto>(apiUrl + _countryId);
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
                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl + _countryId, updatedCountry);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Country successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to update country: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}