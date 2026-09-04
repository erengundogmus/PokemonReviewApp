using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Review
{
    public partial class ReviewUpdateForm : Form
    {
        private readonly IApiService _apiService;
        private int _reviewId;

        public ReviewUpdateForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadReviewForUpdateAsync(int reviewId)
        {
            _reviewId = reviewId;
            try
            {
                var review = await _apiService.GetByIdAsync<ReviewOutputDto>("review", _reviewId);
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
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewUpdate"))
            {
                MessageBox.Show("You do not have permission to update a review.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                bool isSuccess = await _apiService.UpdateAsync("review", _reviewId, updatedReview);

                if (isSuccess)
                {
                    MessageBox.Show("Review successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update review.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}