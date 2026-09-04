using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerDetailForm : Form
    {
        private readonly IApiService _apiService;

        public ReviewerDetailForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadReviewerDetailAsync(int reviewerId)
        {
            if (!UserSession.HasPermission("ReviewerDetail"))
            {
                MessageBox.Show("You do not have permission to view reviewer details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            try
            {
                var reviewer = await _apiService.GetByIdAsync<ReviewerOutputDto>("reviewer", reviewerId);

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