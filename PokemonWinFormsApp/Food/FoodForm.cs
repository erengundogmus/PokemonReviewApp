using PokemonReviewApp.OutputDtos;
using PokemonWinFormsApp.Food;

namespace PokemonWinFormsApp
{
    public partial class FoodForm : Form
    {
        private readonly IApiService _apiService;

        public FoodForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void FoodForm_Load(object sender, EventArgs e)
        {
            await LoadFoodsAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadFoodsAsync();
        }

        private async Task LoadFoodsAsync()
        {
            try
            {
                var foods = await _apiService.GetAllAsync<FoodOutputDto>("food");

                if (foods != null)
                {
                    dataGridView1.DataSource = foods.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedFood = dataGridView1.CurrentRow.DataBoundItem as FoodOutputDto;
                if (selectedFood != null)
                {
                    var detailForm = new FoodDetailForm();
                    await detailForm.LoadFoodDetailAsync(selectedFood.Id);
                    detailForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to FoodOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a food item from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var createForm = new FoodCreateForm();
                createForm.ShowDialog();

                await LoadFoodsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the create form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedFood = dataGridView1.CurrentRow.DataBoundItem as FoodOutputDto;
                if (selectedFood != null)
                {
                    var updateForm = new FoodUpdateForm();
                    await updateForm.LoadFoodForUpdateAsync(selectedFood.Id);
                    updateForm.ShowDialog();

                    await LoadFoodsAsync();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to FoodOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a food item from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedFood = dataGridView1.CurrentRow.DataBoundItem as FoodOutputDto;
                if (selectedFood != null)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        $"Are you sure you want to delete '{selectedFood.Name}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            bool isSuccess = await _apiService.DeleteAsync("food", selectedFood.Id);

                            if (isSuccess)
                            {
                                MessageBox.Show("Food successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadFoodsAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete food.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to FoodOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a food item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}