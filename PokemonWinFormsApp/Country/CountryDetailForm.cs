using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryDetailForm : Form
    {
        private readonly IGenericApiService<CountryInputDto, CountryOutputDto> _countryService;
        private int _countryId;

        public CountryDetailForm(IGenericApiService<CountryInputDto, CountryOutputDto> countryService)
        {
            InitializeComponent();
            _countryService = countryService;
        }

        public async Task LoadCountryDetailAsync(int countryId)
        {
            _countryId = countryId;
            try
            {
                var country = await _countryService.GetByIdAsync("country", _countryId);

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