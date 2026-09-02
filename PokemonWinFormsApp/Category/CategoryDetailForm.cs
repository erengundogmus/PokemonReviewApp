using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Category
{
    public partial class CategoryDetailForm : Form
    {
        private readonly IApiService _apiService;
        private int _categoryId;

        public CategoryDetailForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadCategoryDetailAsync(int categoryId)
        {
            _categoryId = categoryId;
            try
            {
                var category = await _apiService.GetByIdAsync<CategoryOutputDto>("category", _categoryId);

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