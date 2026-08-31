using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerDetailForm : Form
    {
        private readonly IGenericApiService<OwnerInputDto, OwnerOutputDto> _ownerService;
        private int _ownerId;

        public OwnerDetailForm(IGenericApiService<OwnerInputDto, OwnerOutputDto> ownerService)
        {
            InitializeComponent();
            _ownerService = ownerService;
        }

        public async Task LoadOwnerDetailAsync(int ownerId)
        {
            _ownerId = ownerId;
            try
            {
                var owner = await _ownerService.GetByIdAsync("owner", _ownerId);

                if (owner != null)
                {
                    textId.Text = owner.Id.ToString();
                    textName.Text = owner.Name;
                    textGym.Text = owner.Gym;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}