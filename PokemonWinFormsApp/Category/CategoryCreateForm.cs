using PokemonReviewApp.InputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Category
{
    public partial class CategoryCreateForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/category";
        private readonly HttpClient client = new HttpClient();

        public CategoryCreateForm()
        {
            InitializeComponent();
        }

        private async void CategoryCreate_Click(object sender, EventArgs e)
        {
            var newCategory = new CategoryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newCategory);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Category successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to create category: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}