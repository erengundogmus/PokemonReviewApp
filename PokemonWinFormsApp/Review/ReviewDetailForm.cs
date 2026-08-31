using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Review
{
    public partial class ReviewDetailForm : Form
    {
        private readonly IGenericApiService<ReviewInputDto, ReviewOutputDto> _reviewService;
        private int _reviewId;

        public ReviewDetailForm(IGenericApiService<ReviewInputDto, ReviewOutputDto> reviewService)
        {
            InitializeComponent();
            _reviewService = reviewService;
        }

        public async Task LoadReviewDetailAsync(int reviewId)
        {
            _reviewId = reviewId;
            try
            {
                var review = await _reviewService.GetByIdAsync("review", _reviewId);

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