using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryDetailForm : Form
    {
        private readonly IApiService _apiService;
        private int _countryId;

        public CountryDetailForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public async Task LoadCountryDetailAsync(int countryId)
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
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}