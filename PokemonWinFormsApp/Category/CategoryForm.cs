using Microsoft.Extensions.DependencyInjection;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Category
{
    public partial class CategoryForm : Form
    {
        private readonly IGenericApiService<CategoryInputDto, CategoryOutputDto> _categoryService;
        private readonly IServiceProvider _serviceProvider;
        public CategoryForm(IGenericApiService<CategoryInputDto, CategoryOutputDto> categoryService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _serviceProvider = serviceProvider;
        }

        private async void CategoryForm_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync("category");

                if (categories != null)
                {
                    dataGridView1.DataSource = categories.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedCategory = dataGridView1.CurrentRow.DataBoundItem as CategoryOutputDto;
                if (selectedCategory != null)
                {
                    var detailForm = _serviceProvider.GetRequiredService<CategoryDetailForm>();
                    await detailForm.LoadCategoryDetailAsync(selectedCategory.Id);
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
                var createForm = _serviceProvider.GetRequiredService<CategoryCreateForm>();
                createForm.ShowDialog();

                await LoadCategoriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the create form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedCategory = dataGridView1.CurrentRow.DataBoundItem as CategoryOutputDto;
                if (selectedCategory != null)
                {
                    var updateForm = _serviceProvider.GetRequiredService<CategoryUpdateForm>();
                    await updateForm.LoadCategoryForUpdateAsync(selectedCategory.Id);
                    updateForm.ShowDialog();

                    await LoadCategoriesAsync();
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
                            bool isSuccess = await _categoryService.DeleteAsync("category", selectedCategory.Id);

                            if (isSuccess)
                            {
                                MessageBox.Show("Category successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadCategoriesAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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