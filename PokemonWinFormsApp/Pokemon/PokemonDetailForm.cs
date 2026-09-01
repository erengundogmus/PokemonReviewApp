using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonDetailForm : Form
    {
        private readonly IApiService _apiService;
        private int _pokemonId;

        public PokemonDetailForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public async Task LoadPokemonDetailAsync(int pokemonId)
        {
            _pokemonId = pokemonId;
            try
            {
                var pokemon = await _apiService.GetByIdAsync<PokemonOutputDto>("pokemon", _pokemonId);

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