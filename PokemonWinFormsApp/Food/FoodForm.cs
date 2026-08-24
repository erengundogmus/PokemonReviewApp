using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp
{
    public partial class FoodForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/food";
        private readonly HttpClient client = new HttpClient();

        public FoodForm()
        {
            InitializeComponent();
        }
        private async void FoodForm_Load(object sender, EventArgs e)
        {
            await LoadFoodsAsync();
        }

        //list butonuna basıldığında verileri çekecek metod
        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadFoodsAsync();
        }

        private async Task LoadFoodsAsync()
        {
            try
            {
                var foods = await client.GetFromJsonAsync<List<FoodOutputDto>>(apiUrl);

                if (foods != null)
                {
                    dataGridView1.DataSource = foods;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedFood = dataGridView1.CurrentRow.DataBoundItem as FoodOutputDto;
                if (selectedFood != null)
                {
                    PokemonWinFormsApp.Food.FoodDetailForm detailForm = new PokemonWinFormsApp.Food.FoodDetailForm(selectedFood.Id);
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
                PokemonWinFormsApp.Food.FoodCreateForm createForm = new PokemonWinFormsApp.Food.FoodCreateForm();
                createForm.ShowDialog();
                //işlemden sonra otomatik listeyi yeniler
                await LoadFoodsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the create form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedFood = dataGridView1.CurrentRow.DataBoundItem as FoodOutputDto;
                if (selectedFood != null)
                {
                    //gridden seçilen yemeğin idsini alıyoruz
                    PokemonWinFormsApp.Food.FoodUpdateForm updateForm = new PokemonWinFormsApp.Food.FoodUpdateForm(selectedFood.Id);
                    updateForm.ShowDialog();

                    _ = LoadFoodsAsync();
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
                            HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + selectedFood.Id);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Food successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadFoodsAsync();
                            }
                            else
                            {
                                string errorMessage = await response.Content.ReadAsStringAsync();
                                MessageBox.Show("Failed to delete food: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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