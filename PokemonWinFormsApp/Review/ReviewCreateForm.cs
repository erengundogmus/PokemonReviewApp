using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Review
{
    public partial class ReviewCreateForm : Form
    {
        private readonly IGenericApiService<ReviewInputDto, ReviewOutputDto> _reviewService;

        public ReviewCreateForm(IGenericApiService<ReviewInputDto, ReviewOutputDto> reviewService)
        {
            InitializeComponent();
            _reviewService = reviewService;
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
                bool isSuccess = await _reviewService.CreateAsync("review", newReview);

                if (isSuccess)
                {
                    MessageBox.Show("Review successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create review.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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