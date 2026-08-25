using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Review
{
    public partial class ReviewDetailForm : Form
    {
        private readonly int _reviewId;
        private readonly string apiUrl = "https://localhost:7013/api/review/";
        private readonly HttpClient client = new HttpClient();

        public ReviewDetailForm(int reviewId)
        {
            InitializeComponent();
            _reviewId = reviewId;

            //çalışması garanti olsun diye constructor içinde çalıştırdım
            _ = LoadReviewDetailDirectlyAsync();
        }

        private async Task LoadReviewDetailDirectlyAsync()
        {
            try
            {
                var review = await client.GetFromJsonAsync<ReviewOutputDto>(apiUrl + _reviewId);

                if (review != null)
                {
                    textId.Text = review.Id.ToString();
                    textTitle.Text = review.Title;
                    textText.Text = review.Text;
                    textRating.Text = review.Rating.ToString();
                    textPokemonId.Text = review.PokemonId.ToString();
                    textPokemonName.Text = review.PokemonName;
                    textReviewerId.Text = review.ReviewerId.ToString();
                    textReviewerFirstName.Text = review.ReviewerFirstName;
                    textReviewerLastName.Text = review.ReviewerLastName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}