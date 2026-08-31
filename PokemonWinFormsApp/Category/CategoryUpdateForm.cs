using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Category
{
    public partial class CategoryUpdateForm : Form
    {
        private readonly IGenericApiService<CategoryInputDto, CategoryOutputDto> _categoryService;
        private int _categoryId;

        public CategoryUpdateForm(IGenericApiService<CategoryInputDto, CategoryOutputDto> categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;
        }

        public async Task LoadCategoryForUpdateAsync(int categoryId)
        {
            _categoryId = categoryId;
            try
            {
                var category = await _categoryService.GetByIdAsync("category", _categoryId);
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
                bool isSuccess = await _categoryService.UpdateAsync("category", _categoryId, updatedCategory);

                if (isSuccess)
                {
                    MessageBox.Show("Category successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}