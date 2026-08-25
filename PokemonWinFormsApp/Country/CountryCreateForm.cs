using PokemonReviewApp.InputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryCreateForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/country";
        private readonly HttpClient client = new HttpClient();

        public CountryCreateForm()
        {
            InitializeComponent();
        }

        private async void CountryCreate_Click(object sender, EventArgs e)
        {
            var newCountry = new CountryInputDto
            {
                Name = textName.Text,
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newCountry);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Country successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to create country: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
