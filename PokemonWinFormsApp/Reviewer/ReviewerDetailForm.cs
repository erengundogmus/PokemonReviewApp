using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerDetailForm : Form
    {
        private readonly IApiService _apiService;
        private int _reviewerId;

        public ReviewerDetailForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public async Task LoadReviewerDetailAsync(int reviewerId)
        {
            _reviewerId = reviewerId;
            try
            {
                var reviewer = await _apiService.GetByIdAsync<ReviewerOutputDto>("reviewer", _reviewerId);

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