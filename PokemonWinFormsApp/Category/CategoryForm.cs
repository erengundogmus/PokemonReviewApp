using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Category
{
    public partial class CategoryForm : Form
    {

        private readonly string apiUrl = "https://localhost:7013/api/category";
        private readonly HttpClient client = new HttpClient();

        public CategoryForm()
        {
            InitializeComponent();
        }

        private async void CategoryForm_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        //list butonuna basıldığında verileri çekecek metod
        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await client.GetFromJsonAsync<List<CategoryOutputDto>>(apiUrl);

                if (categories != null)
                {
                    dataGridView1.DataSource = categories;
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
                var selectedCategory = dataGridView1.CurrentRow.DataBoundItem as CategoryOutputDto;
                if (selectedCategory != null)
                {
                    PokemonWinFormsApp.Category.CategoryDetailForm detailForm = new PokemonWinFormsApp.Category.CategoryDetailForm(selectedCategory.Id);
                    detailForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to CategoryOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a category item from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonWinFormsApp.Category.CategoryCreateForm createForm = new PokemonWinFormsApp.Category.CategoryCreateForm();
                createForm.ShowDialog();
                //işlemden sonra otomatik listeyi yeniler
                await LoadCategoriesAsync();
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
                var selectedCategory = dataGridView1.CurrentRow.DataBoundItem as CategoryOutputDto;
                if (selectedCategory != null)
                {
                    //gridden id alıyor
                    PokemonWinFormsApp.Category.CategoryUpdateForm updateForm = new PokemonWinFormsApp.Category.CategoryUpdateForm(selectedCategory.Id);
                    updateForm.ShowDialog();

                    _ = LoadCategoriesAsync();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to CategoryOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a category item from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedCategory = dataGridView1.CurrentRow.DataBoundItem as CategoryOutputDto;
                if (selectedCategory != null)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        $"Are you sure you want to delete '{selectedCategory.Name}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + selectedCategory.Id);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Category successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadCategoriesAsync();
                            }
                            else
                            {
                                string errorMessage = await response.Content.ReadAsStringAsync();
                                MessageBox.Show("Failed to delete category: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Could not cast selected row to CategoryOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a category item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }












    }
}