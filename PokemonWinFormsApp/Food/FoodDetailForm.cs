using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Food
{
    public partial class FoodDetailForm : Form
    {
        private readonly IApiService _apiService;

        public FoodDetailForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadFoodDetailAsync(int foodId)
        {
            if (!UserSession.HasPermission("FoodDetail"))
            {
                MessageBox.Show("You do not have permission to view food details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            try
            {
                var food = await _apiService.GetByIdAsync<FoodOutputDto>("food", foodId);

                if (food != null)
                {
                    textId.Text = food.Id.ToString();
                    textName.Text = food.Name;
                    textHp.Text = food.Hp.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}