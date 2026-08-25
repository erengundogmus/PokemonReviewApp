using PokemonReviewApp.Dto;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonCreateForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/pokemon";
        private readonly HttpClient client = new HttpClient();

        public PokemonCreateForm()
        {
            InitializeComponent();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newPokemon = new PokemonInputDto
            {
                Name = textName.Text,
                BirthDate = DateTime.TryParse(textBirthDate.Text, out DateTime birthDate) ? birthDate : DateTime.Now,
                OwnerId = int.TryParse(textOwnerId.Text, out int ownerId) ? ownerId : 0,
                CategoryId = int.TryParse(textCategoryId.Text, out int categoryId) ? categoryId : 0
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newPokemon);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pokemon successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to create pokemon: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}