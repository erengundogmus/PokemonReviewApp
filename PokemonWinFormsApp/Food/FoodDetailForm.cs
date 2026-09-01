using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Food
{
    public partial class FoodDetailForm : Form
    {
        private readonly IApiService _apiService;
        private int _foodId;

        public FoodDetailForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public async Task LoadFoodDetailAsync(int foodId)
        {
            _foodId = foodId;
            try
            {
                var food = await _apiService.GetByIdAsync<FoodOutputDto>("food", _foodId);

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