using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonDetailForm : Form
    {
        private readonly int _pokemonId;
        private readonly string apiUrl = "https://localhost:7013/api/pokemon/";
        private readonly HttpClient client = new HttpClient();

        public PokemonDetailForm(int pokemonId)
        {
            InitializeComponent();
            _pokemonId = pokemonId;

            _ = LoadPokemonDetailDirectlyAsync();
        }

        private async Task LoadPokemonDetailDirectlyAsync()
        {
            try
            {
                var pokemon = await client.GetFromJsonAsync<PokemonOutputDto>(apiUrl + _pokemonId);

                if (pokemon != null)
                {
                    textId.Text = pokemon.Id.ToString();
                    textName.Text = pokemon.Name;
                    textBirthDate.Text = pokemon.BirthDate.ToString("yyyy-MM-dd");
                    textOwnerId.Text = pokemon.OwnerId.ToString();
                    textOwnerName.Text = pokemon.OwnerName;
                    textCategoryId.Text = pokemon.CategoryId.ToString();
                    textCategoryName.Text = pokemon.CategoryName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}