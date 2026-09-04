using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonDetailForm : Form
    {
        private readonly IApiService _apiService;

        public PokemonDetailForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadPokemonDetailAsync(int pokemonId)
        {
            if (!UserSession.HasPermission("PokemonDetail"))
            {
                MessageBox.Show("You do not have permission to view pokemon details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            try
            {
                var pokemon = await _apiService.GetByIdAsync<PokemonOutputDto>("pokemon", pokemonId);

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