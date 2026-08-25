using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerDetailForm : Form
    {
        private readonly int _reviewerId;
        private readonly string apiUrl = "https://localhost:7013/api/reviewer/";
        private readonly HttpClient client = new HttpClient();

        public ReviewerDetailForm(int reviewerId)
        {
            InitializeComponent();
            _reviewerId = reviewerId;

            // kesin çalışması için load olayını beklemeden constructor içinde tetikleyelim
            _ = LoadReviewerDetailDirectlyAsync();
        }

        private async Task LoadReviewerDetailDirectlyAsync()
        {
            try
            {
                var reviewer = await client.GetFromJsonAsync<ReviewerOutputDto>(apiUrl + _reviewerId);

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