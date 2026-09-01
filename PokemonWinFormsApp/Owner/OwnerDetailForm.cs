using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerDetailForm : Form
    {
        private readonly IApiService _apiService;
        private int _ownerId;

        public OwnerDetailForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public async Task LoadOwnerDetailAsync(int ownerId)
        {
            _ownerId = ownerId;
            try
            {
                var owner = await _apiService.GetByIdAsync<OwnerOutputDto>("owner", _ownerId);

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