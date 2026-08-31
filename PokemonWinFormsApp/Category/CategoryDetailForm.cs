using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Category
{
    public partial class CategoryDetailForm : Form
    {
        private readonly IGenericApiService<CategoryInputDto, CategoryOutputDto> _categoryService;
        private int _categoryId;
        public CategoryDetailForm(IGenericApiService<CategoryInputDto, CategoryOutputDto> categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;
        }

        public async Task LoadCategoryDetailAsync(int categoryId)
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
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}