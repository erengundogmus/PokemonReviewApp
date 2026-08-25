using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Review
{
    public partial class ReviewUpdateForm : Form
    {
        private readonly int _reviewId;
        private readonly string apiUrl = "https://localhost:7013/api/review/";
        private readonly HttpClient client = new HttpClient();

        public ReviewUpdateForm(int reviewId)
        {
            InitializeComponent();
            _reviewId = reviewId;

            // form açıldığı an mevcut bilgileri form kutularına doldurur
            _ = LoadReviewDataAsync();
        }

        private async Task LoadReviewDataAsync()
        {
            try
            {
                var review = await client.GetFromJsonAsync<ReviewOutputDto>(apiUrl + _reviewId);
                if (review != null)
                {
                    textTitle.Text = review.Title;
                    textText.Text = review.Text;
                    textRating.Text = review.Rating.ToString();
                    textReviewerId.Text = review.ReviewerId.ToString();
                    textPokemonId.Text = review.PokemonId.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedReview = new ReviewInputDto
            {
                Title = textTitle.Text,
                Text = textText.Text,
                Rating = int.TryParse(textRating.Text, out int rating) ? rating : 0,
                ReviewerId = int.TryParse(textReviewerId.Text, out int reviewerId) ? reviewerId : 0,
                PokemonId = int.TryParse(textPokemonId.Text, out int pokemonId) ? pokemonId : 0
            };

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl + _reviewId, updatedReview);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Review successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to update review: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}