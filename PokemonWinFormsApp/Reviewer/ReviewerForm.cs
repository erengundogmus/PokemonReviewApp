using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp
{
    public partial class ReviewerForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/reviewer";
        private readonly HttpClient client = new HttpClient();

        public ReviewerForm()
        {
            InitializeComponent();
        }
        private async void ReviewerForm_Load(object sender, EventArgs e)
        {
            await LoadReviewersAsync();
        }

        //list butonuna basıldığında verileri çekecek metod
        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadReviewersAsync();
        }

        private async Task LoadReviewersAsync()
        {
            try
            {
                var reviewers = await client.GetFromJsonAsync<List<ReviewerOutputDto>>(apiUrl);

                if (reviewers != null)
                {
                    dataGridView1.DataSource = reviewers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedReviewer = dataGridView1.CurrentRow.DataBoundItem as ReviewerOutputDto;
                if (selectedReviewer != null)
                {
                    PokemonWinFormsApp.Reviewer.ReviewerDetailForm detailForm = new PokemonWinFormsApp.Reviewer.ReviewerDetailForm(selectedReviewer.Id);
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
            try
            {
                PokemonWinFormsApp.Reviewer.ReviewerCreateForm createForm = new PokemonWinFormsApp.Reviewer.ReviewerCreateForm();
                createForm.ShowDialog();
                //işlemden sonra otomatik listeyi yeniler
                await LoadReviewersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the create form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedReviewer = dataGridView1.CurrentRow.DataBoundItem as ReviewerOutputDto;
                if (selectedReviewer != null)
                {
                    //gridden seçilen reviewer'ın id'sini alıyoruz
                    PokemonWinFormsApp.Reviewer.ReviewerUpdateForm updateForm = new PokemonWinFormsApp.Reviewer.ReviewerUpdateForm(selectedReviewer.Id);
                    updateForm.ShowDialog();

                    _ = LoadReviewersAsync();
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
                            HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + selectedReviewer.Id);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Reviewer successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadReviewersAsync();
                            }
                            else
                            {
                                string errorMessage = await response.Content.ReadAsStringAsync();
                                MessageBox.Show("Failed to delete reviewer: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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