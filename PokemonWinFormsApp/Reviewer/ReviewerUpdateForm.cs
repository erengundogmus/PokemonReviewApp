using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Reviewer
{
    public partial class ReviewerUpdateForm : Form
    {
        private readonly int _reviewerId;
        private readonly string apiUrl = "https://localhost:7013/api/reviewer/";
        private readonly HttpClient client = new HttpClient();

        public ReviewerUpdateForm(int reviewerId)
        {
            InitializeComponent();
            _reviewerId = reviewerId;

            // form açıldığı an mevcut bilgileri form kutularına doldurur
            _ = LoadReviewerDataAsync();
        }

        private async Task LoadReviewerDataAsync()
        {
            try
            {
                var reviewer = await client.GetFromJsonAsync<ReviewerOutputDto>(apiUrl + _reviewerId);
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
                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl + _reviewerId, updatedReviewer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Reviewer successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to update reviewer: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}