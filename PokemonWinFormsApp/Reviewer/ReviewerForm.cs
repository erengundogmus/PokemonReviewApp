using PokemonReviewApp.OutputDtos;
using PokemonWinFormsApp.Reviewer;

namespace PokemonWinFormsApp
{
    public partial class ReviewerForm : Form
    {
        private readonly IApiService _apiService;

        public ReviewerForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void ReviewerForm_Load(object sender, EventArgs e)
        {
            await LoadReviewersAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadReviewersAsync();
        }

        private async Task LoadReviewersAsync()
        {
            try
            {
                var reviewers = await _apiService.GetAllAsync<ReviewerOutputDto>("reviewer");

                if (reviewers != null)
                {
                    dataGridView1.DataSource = reviewers.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonDetail_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewerDetail"))
            {
                MessageBox.Show("You do not have permission to view reviewer details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                var selectedReviewer = dataGridView1.CurrentRow.DataBoundItem as ReviewerOutputDto;
                if (selectedReviewer != null)
                {
                    var detailForm = new ReviewerDetailForm();
                    await detailForm.LoadReviewerDetailAsync(selectedReviewer.Id);
                    detailForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to ReviewerOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a reviewer item from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewerCreate"))
            {
                MessageBox.Show("You do not have permission to create a reviewer.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var createForm = new ReviewerCreateForm();
                createForm.ShowDialog();

                await LoadReviewersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the create form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewerUpdate"))
            {
                MessageBox.Show("You do not have permission to update a reviewer.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                var selectedReviewer = dataGridView1.CurrentRow.DataBoundItem as ReviewerOutputDto;
                if (selectedReviewer != null)
                {
                    var updateForm = new ReviewerUpdateForm();
                    await updateForm.LoadReviewerForUpdateAsync(selectedReviewer.Id);
                    updateForm.ShowDialog();

                    await LoadReviewersAsync();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to ReviewerOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a reviewer item from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewerDelete"))
            {
                MessageBox.Show("You do not have permission to delete a reviewer.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                var selectedReviewer = dataGridView1.CurrentRow.DataBoundItem as ReviewerOutputDto;
                if (selectedReviewer != null)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        $"Are you sure you want to delete '{selectedReviewer.FirstName} {selectedReviewer.LastName}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            bool isSuccess = await _apiService.DeleteAsync("reviewer", selectedReviewer.Id);

                            if (isSuccess)
                            {
                                MessageBox.Show("Reviewer successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadReviewersAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete reviewer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Could not cast selected row to ReviewerOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a reviewer item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}