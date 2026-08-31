using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Food
{
    public partial class FoodDetailForm : Form
    {
        private readonly IGenericApiService<FoodInputDto, FoodOutputDto> _foodService;
        private int _foodId;

        public FoodDetailForm(IGenericApiService<FoodInputDto, FoodOutputDto> foodService)
        {
            InitializeComponent();
            _foodService = foodService;
        }

        public async Task LoadFoodDetailAsync(int foodId)
        {
            _foodId = foodId;
            try
            {
                var food = await _foodService.GetByIdAsync("food", _foodId);

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