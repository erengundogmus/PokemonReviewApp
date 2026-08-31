using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Category
{
    public partial class CategoryCreateForm : Form
    {
        private readonly IGenericApiService<CategoryInputDto, CategoryOutputDto> _categoryService;
        public CategoryCreateForm(IGenericApiService<CategoryInputDto, CategoryOutputDto> categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;
        }

        private async void CategoryCreate_Click(object sender, EventArgs e)
        {
            var newCategory = new CategoryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                //httpclient ve baseurl yok sadece endpoint ismini veriyoruz
                bool isSuccess = await _categoryService.CreateAsync("category", newCategory);

                if (isSuccess)
                {
                    MessageBox.Show("Category successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    // Generic servis bool döndüğü için hata mesajını genel tutuyoruz
                    MessageBox.Show("Failed to create category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}