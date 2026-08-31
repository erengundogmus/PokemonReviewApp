using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Food
{
    public partial class FoodUpdateForm : Form
    {
        private readonly IGenericApiService<FoodInputDto, FoodOutputDto> _foodService;
        private int _foodId;

        public FoodUpdateForm(IGenericApiService<FoodInputDto, FoodOutputDto> foodService)
        {
            InitializeComponent();
            _foodService = foodService;
        }

        public async Task LoadFoodForUpdateAsync(int foodId)
        {
            _foodId = foodId;
            try
            {
                var food = await _foodService.GetByIdAsync("food", _foodId);
                if (food != null)
                {
                    textName.Text = food.Name;
                    textHp.Text = food.Hp.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedFood = new FoodInputDto
            {
                Name = textName.Text,
                Hp = int.TryParse(textHp.Text, out int hp) ? hp : 0
            };

            try
            {
                bool isSuccess = await _foodService.UpdateAsync("food", _foodId, updatedFood);

                if (isSuccess)
                {
                    MessageBox.Show("Food successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update food.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}