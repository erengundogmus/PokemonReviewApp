using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerDetailForm : Form
    {
        private readonly IGenericApiService<ReviewerInputDto, ReviewerOutputDto> _reviewerService;
        private int _reviewerId;

        public ReviewerDetailForm(IGenericApiService<ReviewerInputDto, ReviewerOutputDto> reviewerService)
        {
            InitializeComponent();
            _reviewerService = reviewerService;
        }

        public async Task LoadReviewerDetailAsync(int reviewerId)
        {
            _reviewerId = reviewerId;
            try
            {
                var reviewer = await _reviewerService.GetByIdAsync("reviewer", _reviewerId);

                if (reviewer != null)
                {
                    textId.Text = reviewer.Id.ToString();
                    textFirstName.Text = reviewer.FirstName;
                    textLastName.Text = reviewer.LastName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}