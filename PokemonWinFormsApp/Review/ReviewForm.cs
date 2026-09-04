using PokemonReviewApp.OutputDtos;
using PokemonWinFormsApp.Review;

namespace PokemonWinFormsApp
{
    public partial class ReviewForm : Form
    {
        private readonly IApiService _apiService;

        public ReviewForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
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
                var reviews = await _apiService.GetAllAsync<ReviewOutputDto>("review");

                if (reviews != null)
                {
                    dataGridView1.DataSource = reviews.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonDetail_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewDetail"))
            {
                MessageBox.Show("You do not have permission to view review details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                var selectedReview = dataGridView1.CurrentRow.DataBoundItem as ReviewOutputDto;
                if (selectedReview != null)
                {
                    var detailForm = new ReviewDetailForm();
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
            if (!UserSession.HasPermission("ReviewCreate"))
            {
                MessageBox.Show("You do not have permission to create a review.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var createForm = new ReviewCreateForm();
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
            if (!UserSession.HasPermission("ReviewUpdate"))
            {
                MessageBox.Show("You do not have permission to update a review.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                var selectedReview = dataGridView1.CurrentRow.DataBoundItem as ReviewOutputDto;
                if (selectedReview != null)
                {
                    var updateForm = new ReviewUpdateForm();
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
            if (!UserSession.HasPermission("ReviewDelete"))
            {
                MessageBox.Show("You do not have permission to delete a review.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                            bool isSuccess = await _apiService.DeleteAsync("review", selectedReview.Id);

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