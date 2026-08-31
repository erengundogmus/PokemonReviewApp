using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerCreateForm : Form
    {
        private readonly IGenericApiService<OwnerInputDto, OwnerOutputDto> _ownerService;

        public OwnerCreateForm(IGenericApiService<OwnerInputDto, OwnerOutputDto> ownerService)
        {
            InitializeComponent();
            _ownerService = ownerService;
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newOwner = new OwnerInputDto
            {
                Name = textName.Text,
                Gym = textGym.Text,
                CountryId = int.TryParse(textCountryId.Text, out int countryId) ? countryId : 0
            };

            try
            {
                bool isSuccess = await _ownerService.CreateAsync("owner", newOwner);

                if (isSuccess)
                {
                    MessageBox.Show("Owner successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create owner.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OwnerCreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}