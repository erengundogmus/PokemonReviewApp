using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerUpdateForm : Form
    {
        private readonly IGenericApiService<OwnerInputDto, OwnerOutputDto> _ownerService;
        private int _ownerId;

        public OwnerUpdateForm(IGenericApiService<OwnerInputDto, OwnerOutputDto> ownerService)
        {
            InitializeComponent();
            _ownerService = ownerService;
        }

        public async Task LoadOwnerForUpdateAsync(int ownerId)
        {
            _ownerId = ownerId;
            try
            {
                var owner = await _ownerService.GetByIdAsync("owner", _ownerId);
                if (owner != null)
                {
                    textName.Text = owner.Name;
                    textGym.Text = owner.Gym;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedOwner = new OwnerInputDto
            {
                Name = textName.Text,
                Gym = textGym.Text,
                CountryId = int.TryParse(textCountryId.Text, out int countryId) ? countryId : 0
            };

            try
            {
                bool isSuccess = await _ownerService.UpdateAsync("owner", _ownerId, updatedOwner);

                if (isSuccess)
                {
                    MessageBox.Show("Owner successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update owner.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}