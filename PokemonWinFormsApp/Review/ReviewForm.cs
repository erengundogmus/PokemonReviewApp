using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp
{
    public partial class ReviewForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/review";
        private readonly HttpClient client = new HttpClient();

        public ReviewForm()
        {
            InitializeComponent();
        }
        private async void ReviewForm_Load(object sender, EventArgs e)
        {
            await LoadReviewsAsync();
        }

        //list butonuna basıldığında verileri çekecek metod
        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadReviewsAsync();
        }

        private async Task LoadReviewsAsync()
        {
            try
            {
                var reviews = await client.GetFromJsonAsync<List<ReviewOutputDto>>(apiUrl);

                if (reviews != null)
                {
                    dataGridView1.DataSource = reviews;
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
                var selectedReview = dataGridView1.CurrentRow.DataBoundItem as ReviewOutputDto;
                if (selectedReview != null)
                {
                    PokemonWinFormsApp.Review.ReviewDetailForm detailForm = new PokemonWinFormsApp.Review.ReviewDetailForm(selectedReview.Id);
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
                PokemonWinFormsApp.Review.ReviewCreateForm createForm = new PokemonWinFormsApp.Review.ReviewCreateForm();
                createForm.ShowDialog();
                //işlemden sonra otomatik listeyi yeniler
                await LoadReviewsAsync();
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
                var selectedReview = dataGridView1.CurrentRow.DataBoundItem as ReviewOutputDto;
                if (selectedReview != null)
                {
                    //gridden seçilen reviewin idsini alıyoruz
                    PokemonWinFormsApp.Review.ReviewUpdateForm updateForm = new PokemonWinFormsApp.Review.ReviewUpdateForm(selectedReview.Id);
                    updateForm.ShowDialog();

                    _ = LoadReviewsAsync();
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
                            HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + selectedReview.Id);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Review successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadReviewsAsync();
                            }
                            else
                            {
                                string errorMessage = await response.Content.ReadAsStringAsync();
                                MessageBox.Show("Failed to delete review: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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