using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Category
{
    public partial class CategoryUpdateForm : Form
    {
        private readonly int _categoryId;
        private readonly string apiUrl = "https://localhost:7013/api/category/";
        private readonly HttpClient client = new HttpClient();

        public CategoryUpdateForm(int categoryId)
        {
            InitializeComponent();
            _categoryId = categoryId;

            //form açıldığı an mevcut bilgileri form kutularına doldurur
            _ = LoadCategoryDataAsync();
        }

        private async Task LoadCategoryDataAsync()
        {
            try
            {
                var category = await client.GetFromJsonAsync<CategoryOutputDto>(apiUrl + _categoryId);
                if (category != null)
                {
                    textName.Text = category.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedCategory = new CategoryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl + _categoryId, updatedCategory);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Category successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to update category: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}