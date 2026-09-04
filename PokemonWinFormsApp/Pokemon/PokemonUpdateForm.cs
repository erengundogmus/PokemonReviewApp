using PokemonReviewApp.Dto;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonUpdateForm : Form
    {
        private readonly IApiService _apiService;
        private int _pokemonId;

        public PokemonUpdateForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadPokemonForUpdateAsync(int pokemonId)
        {
            _pokemonId = pokemonId;
            try
            {
                var pokemon = await _apiService.GetByIdAsync<PokemonOutputDto>("pokemon", _pokemonId);
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
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("PokemonUpdate"))
            {
                MessageBox.Show("You do not have permission to update a pokemon.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedPokemon = new PokemonInputDto
            {
                Name = textName.Text,
                BirthDate = DateTime.TryParse(textBirthDate.Text, out DateTime birthDate) ? birthDate : DateTime.Now,
                OwnerId = int.TryParse(textOwnerId.Text, out int ownerId) ? ownerId : 0,
                CategoryId = int.TryParse(textCategoryId.Text, out int categoryId) ? categoryId : 0
            };

            try
            {
                bool isSuccess = await _apiService.UpdateAsync("pokemon", _pokemonId, updatedPokemon);

                if (isSuccess)
                {
                    MessageBox.Show("Pokemon successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update pokemon.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}