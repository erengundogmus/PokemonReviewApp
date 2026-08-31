using PokemonReviewApp.Dto;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonDetailForm : Form
    {
        private readonly IGenericApiService<PokemonInputDto, PokemonOutputDto> _pokemonService;
        private int _pokemonId;

        public PokemonDetailForm(IGenericApiService<PokemonInputDto, PokemonOutputDto> pokemonService)
        {
            InitializeComponent();
            _pokemonService = pokemonService;
        }

        public async Task LoadPokemonDetailAsync(int pokemonId)
        {
            _pokemonId = pokemonId;
            try
            {
                var pokemon = await _pokemonService.GetByIdAsync("pokemon", _pokemonId);

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