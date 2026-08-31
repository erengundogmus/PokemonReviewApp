using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryCreateForm : Form
    {
        private readonly IGenericApiService<CountryInputDto, CountryOutputDto> _countryService;

        public CountryCreateForm(IGenericApiService<CountryInputDto, CountryOutputDto> countryService)
        {
            InitializeComponent();
            _countryService = countryService;
        }

        private async void CountryCreate_Click(object sender, EventArgs e)
        {
            var newCountry = new CountryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                bool isSuccess = await _countryService.CreateAsync("country", newCountry);

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