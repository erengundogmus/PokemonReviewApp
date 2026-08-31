using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerCreateForm : Form
    {
        private readonly IGenericApiService<ReviewerInputDto, ReviewerOutputDto> _reviewerService;

        public ReviewerCreateForm(IGenericApiService<ReviewerInputDto, ReviewerOutputDto> reviewerService)
        {
            InitializeComponent();
            _reviewerService = reviewerService;
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newReviewer = new ReviewerInputDto
            {
                FirstName = textFirstName.Text,
                LastName = textLastName.Text
            };

            try
            {
                bool isSuccess = await _reviewerService.CreateAsync("reviewer", newReviewer);

                if (isSuccess)
                {
                    MessageBox.Show("Reviewer successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create reviewer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReviewerCreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}