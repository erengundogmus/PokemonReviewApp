using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Category

{
    public partial class CategoryDetailForm : Form
    {
        private readonly int _categoryId;
        private readonly string apiUrl = "https://localhost:7013/api/category/";
        private readonly HttpClient client = new HttpClient();

        public CategoryDetailForm(int categoryId)
        {
            InitializeComponent();
            _categoryId = categoryId;

            //kesin çalışması için load olayını beklemeden constructor içinde tetikleyelim
            _ = LoadCategoryDetailDirectlyAsync();
        }

        private async Task LoadCategoryDetailDirectlyAsync()
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
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}