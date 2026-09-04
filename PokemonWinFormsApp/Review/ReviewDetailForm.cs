using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Review
{
    public partial class ReviewDetailForm : Form
    {
        private readonly IApiService _apiService;

        public ReviewDetailForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadReviewDetailAsync(int reviewId)
        {
            if (!UserSession.HasPermission("ReviewDetail"))
            {
                MessageBox.Show("You do not have permission to view review details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            try
            {
                var review = await _apiService.GetByIdAsync<ReviewOutputDto>("review", reviewId);

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