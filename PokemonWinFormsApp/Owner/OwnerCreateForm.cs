using PokemonReviewApp.InputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerCreateForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/owner";
        private readonly HttpClient client = new HttpClient();

        public OwnerCreateForm()
        {
            InitializeComponent();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newOwner = new OwnerInputDto
            {
                Name = textName.Text,
                Gym = textGym.Text,
                CountryId = int.TryParse(textCountryId.Text, out int countryId) ? countryId : 0
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newOwner);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Owner successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to create owner: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OwnerCreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}