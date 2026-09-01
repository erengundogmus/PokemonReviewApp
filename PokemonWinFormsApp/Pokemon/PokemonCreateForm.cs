using PokemonReviewApp.Dto;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonCreateForm : Form
    {
        private readonly IApiService _apiService;

        public PokemonCreateForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
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
                bool isSuccess = await _apiService.CreateAsync("pokemon", newPokemon);

                if (isSuccess)
                {
                    MessageBox.Show("Pokemon successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create pokemon.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}