using PokemonReviewApp.InputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Review
{
    public partial class ReviewCreateForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/review";
        private readonly HttpClient client = new HttpClient();

        public ReviewCreateForm()
        {
            InitializeComponent();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newReview = new ReviewInputDto
            {
                Title = textTitle.Text,
                Text = textText.Text,
                Rating = int.TryParse(textRating.Text, out int rating) ? rating : 0,
                ReviewerId = int.TryParse(textReviewerId.Text, out int reviewerId) ? reviewerId : 0,
                PokemonId = int.TryParse(textPokemonId.Text, out int pokemonId) ? pokemonId : 0
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newReview);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Review successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to create review: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReviewCreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}