using PokemonReviewApp.Dto;
using PokemonReviewApp.OutputDtos;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonUpdateForm : Form
    {
        private readonly int _pokemonId;
        private readonly string apiUrl = "https://localhost:7013/api/pokemon/";
        private readonly HttpClient client = new HttpClient();

        public PokemonUpdateForm(int pokemonId)
        {
            InitializeComponent();
            _pokemonId = pokemonId;

            // form açıldığı an mevcut bilgileri form kutularına doldurur
            _ = LoadPokemonDataAsync();
        }

        private async Task LoadPokemonDataAsync()
        {
            try
            {
                var pokemon = await client.GetFromJsonAsync<PokemonOutputDto>(apiUrl + _pokemonId);
                if (pokemon != null)
                {
                    textName.Text = pokemon.Name;
                    textBirthDate.Text = pokemon.BirthDate.ToString("dd-MM-yyyy");
                    textOwnerId.Text = pokemon.OwnerId.ToString();
                    textCategoryId.Text = pokemon.CategoryId.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedPokemon = new PokemonInputDto
            {
                Name = textName.Text,
                BirthDate = DateTime.TryParse(textBirthDate.Text, out DateTime birthDate) ? birthDate : DateTime.Now,
                OwnerId = int.TryParse(textOwnerId.Text, out int ownerId) ? ownerId : 0,
                CategoryId = int.TryParse(textCategoryId.Text, out int categoryId) ? categoryId : 0
            };

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserSession.Token);

                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl + _pokemonId, updatedPokemon);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pokemon successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to update pokemon: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}