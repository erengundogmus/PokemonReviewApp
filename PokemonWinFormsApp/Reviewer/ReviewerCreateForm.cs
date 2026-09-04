using PokemonReviewApp.InputDtos;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerCreateForm : Form
    {
        private readonly IApiService _apiService;

        public ReviewerCreateForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewerCreate"))
            {
                MessageBox.Show("You do not have permission to create a reviewer.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newReviewer = new ReviewerInputDto
            {
                FirstName = textFirstName.Text,
                LastName = textLastName.Text
            };

            try
            {
                bool isSuccess = await _apiService.CreateAsync("reviewer", newReviewer);

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