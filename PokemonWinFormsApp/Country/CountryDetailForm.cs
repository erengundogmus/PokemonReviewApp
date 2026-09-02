using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryDetailForm : Form
    {
        private readonly IApiService _apiService;

        public CountryDetailForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadCountryDetailAsync(int countryId)
        {
            try
            {
                var country = await _apiService.GetByIdAsync<CountryOutputDto>("country", countryId);

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