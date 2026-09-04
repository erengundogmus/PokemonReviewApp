using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerDetailForm : Form
    {
        private readonly IApiService _apiService;
        private int _ownerId;

        public OwnerDetailForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadOwnerDetailAsync(int ownerId)
        {
            if (!UserSession.HasPermission("OwnerDetail"))
            {
                MessageBox.Show("You do not have permission to view owner details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

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