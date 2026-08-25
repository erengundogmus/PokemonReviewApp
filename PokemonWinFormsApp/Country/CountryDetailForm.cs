using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Country

{
    public partial class CountryDetailForm : Form
    {
        private readonly int _countryId;
        private readonly string apiUrl = "https://localhost:7013/api/country/";
        private readonly HttpClient client = new HttpClient();

        public CountryDetailForm(int countryId)
        {
            InitializeComponent();
            _countryId = countryId;

            //kesin çalışması için load olayını beklemeden constructor içinde tetikleyelim
            _ = LoadCountryDetailDirectlyAsync();
        }

        private async Task LoadCountryDetailDirectlyAsync()
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
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}