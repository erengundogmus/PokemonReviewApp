using Microsoft.Extensions.DependencyInjection;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using PokemonWinFormsApp.Review;

namespace PokemonWinFormsApp
{
    public partial class ReviewForm : Form
    {
        private readonly IGenericApiService<ReviewInputDto, ReviewOutputDto> _reviewService;
        private readonly IServiceProvider _serviceProvider;

        public ReviewForm(IGenericApiService<ReviewInputDto, ReviewOutputDto> reviewService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _reviewService = reviewService;
            _serviceProvider = serviceProvider;
        }

        private async void ReviewForm_Load(object sender, EventArgs e)
        {
            await LoadReviewsAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadReviewsAsync();
        }

        private async Task LoadReviewsAsync()
        {
            try
            {
                var reviews = await _reviewService.GetAllAsync("review");

                if (reviews != null)
                {
                    dataGridView1.DataSource = reviews.ToList();
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
                var selectedReview = dataGridView1.CurrentRow.DataBoundItem as ReviewOutputDto;
                if (selectedReview != null)
                {
                    var detailForm = _serviceProvider.GetRequiredService<ReviewDetailForm>();
                    await detailForm.LoadReviewDetailAsync(selectedReview.Id);
                    detailForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to ReviewOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a review item from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var createForm = _serviceProvider.GetRequiredService<ReviewCreateForm>();
                createForm.ShowDialog();
                await LoadReviewsAsync();
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
                var selectedReview = dataGridView1.CurrentRow.DataBoundItem as ReviewOutputDto;
                if (selectedReview != null)
                {
                    var updateForm = _serviceProvider.GetRequiredService<ReviewUpdateForm>();
                    await updateForm.LoadReviewForUpdateAsync(selectedReview.Id);
                    updateForm.ShowDialog();

                    await LoadReviewsAsync();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to ReviewOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a review item from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedReview = dataGridView1.CurrentRow.DataBoundItem as ReviewOutputDto;
                if (selectedReview != null)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        $"Are you sure you want to delete '{selectedReview.Title}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            bool isSuccess = await _reviewService.DeleteAsync("review", selectedReview.Id);

                            if (isSuccess)
                            {
                                MessageBox.Show("Review successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadReviewsAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete review.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Could not cast selected row to ReviewOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a review item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}