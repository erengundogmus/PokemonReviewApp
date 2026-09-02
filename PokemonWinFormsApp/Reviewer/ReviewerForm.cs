using Autofac;
using PokemonReviewApp.OutputDtos;
using PokemonWinFormsApp.Reviewer;

namespace PokemonWinFormsApp
{
    public partial class ReviewerForm : Form
    {
        private readonly IApiService _apiService;

        public ReviewerForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
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
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedReviewer = dataGridView1.CurrentRow.DataBoundItem as ReviewerOutputDto;
                if (selectedReviewer != null)
                {
                    //autofac ile güvenli form çağırmak için child scope açıyor
                    using (var scope = Program.Container.BeginLifetimeScope())
                    {
                        var detailForm = scope.Resolve<ReviewerDetailForm>();
                        await detailForm.LoadReviewerDetailAsync(selectedReviewer.Id);
                        detailForm.ShowDialog();
                    }
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
            try
            {
                //autofac ile güvenli form çağırmak için child scope açıyor
                using (var scope = Program.Container.BeginLifetimeScope())
                {
                    var createForm = scope.Resolve<ReviewerCreateForm>();
                    createForm.ShowDialog();
                }
                await LoadReviewersAsync();
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
                var selectedReviewer = dataGridView1.CurrentRow.DataBoundItem as ReviewerOutputDto;
                if (selectedReviewer != null)
                {
                    //autofac ile güvenli form çağırmak için child scope açıyor
                    using (var scope = Program.Container.BeginLifetimeScope())
                    {
                        var updateForm = scope.Resolve<ReviewerUpdateForm>();
                        await updateForm.LoadReviewerForUpdateAsync(selectedReviewer.Id);
                        updateForm.ShowDialog();
                    }

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