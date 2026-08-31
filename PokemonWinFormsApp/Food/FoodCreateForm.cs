using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Food
{
    public partial class FoodCreateForm : Form
    {
        private readonly IGenericApiService<FoodInputDto, FoodOutputDto> _foodService;

        public FoodCreateForm(IGenericApiService<FoodInputDto, FoodOutputDto> foodService)
        {
            InitializeComponent();
            _foodService = foodService;
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newFood = new FoodInputDto
            {
                Name = textName.Text,
                Hp = int.TryParse(textHp.Text, out int hp) ? hp : 0
            };

            try
            {
                bool isSuccess = await _foodService.CreateAsync("food", newFood);

                if (isSuccess)
                {
                    MessageBox.Show("Food successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create food.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FoodCreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}