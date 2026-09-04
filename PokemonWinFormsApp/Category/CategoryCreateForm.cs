using PokemonReviewApp.InputDtos;

namespace PokemonWinFormsApp.Category
{
    public partial class CategoryCreateForm : Form
    {
        private readonly IApiService _apiService;

        public CategoryCreateForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void CategoryCreate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CategoryCreate"))
            {
                MessageBox.Show("You do not have permission to create a category.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCategory = new CategoryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                bool isSuccess = await _apiService.CreateAsync("category", newCategory);

                if (isSuccess)
                {
                    MessageBox.Show("Category successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
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