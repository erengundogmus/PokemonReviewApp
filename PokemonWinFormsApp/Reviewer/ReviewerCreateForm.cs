using PokemonReviewApp.InputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerCreateForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/reviewer";
        private readonly HttpClient client = new HttpClient();

        public ReviewerCreateForm()
        {
            InitializeComponent();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newReviewer = new ReviewerInputDto
            {
                FirstName = textFirstName.Text,
                LastName = textLastName.Text
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newReviewer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Reviewer successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to create reviewer: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReviewerCreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}