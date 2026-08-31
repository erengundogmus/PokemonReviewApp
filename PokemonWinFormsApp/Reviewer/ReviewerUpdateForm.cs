using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerUpdateForm : Form
    {
        private readonly IGenericApiService<ReviewerInputDto, ReviewerOutputDto> _reviewerService;
        private int _reviewerId;

        public ReviewerUpdateForm(IGenericApiService<ReviewerInputDto, ReviewerOutputDto> reviewerService)
        {
            InitializeComponent();
            _reviewerService = reviewerService;
        }

        public async Task LoadReviewerForUpdateAsync(int reviewerId)
        {
            _reviewerId = reviewerId;
            try
            {
                var reviewer = await _reviewerService.GetByIdAsync("reviewer", _reviewerId);
                if (reviewer != null)
                {
                    textFirstName.Text = reviewer.FirstName;
                    textLastName.Text = reviewer.LastName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedReviewer = new ReviewerInputDto
            {
                FirstName = textFirstName.Text,
                LastName = textLastName.Text
            };

            try
            {
                bool isSuccess = await _reviewerService.UpdateAsync("reviewer", _reviewerId, updatedReviewer);

                if (isSuccess)
                {
                    MessageBox.Show("Reviewer successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update reviewer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}