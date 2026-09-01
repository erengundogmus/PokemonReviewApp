using Microsoft.Extensions.DependencyInjection;
using PokemonWinFormsApp.Category;
using PokemonWinFormsApp.Country;
using PokemonWinFormsApp.Pokemon;
using PokemonWinFormsApp.PokemonFood;

namespace PokemonWinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void buttonFood_Click(object sender, EventArgs e)
        {
            var foodForm = _serviceProvider.GetRequiredService<FoodForm>();
            foodForm.ShowDialog();
        }

        private void buttonCountry_Click(object sender, EventArgs e)
        {
            var countryForm = _serviceProvider.GetRequiredService<CountryForm>();
            countryForm.ShowDialog();
        }

        private void buttonCategory_Click(object sender, EventArgs e)
        {
            var categoryForm = _serviceProvider.GetRequiredService<CategoryForm>();
            categoryForm.ShowDialog();
        }

        private void buttonOwner_Click(object sender, EventArgs e)
        {
            var ownerForm = _serviceProvider.GetRequiredService<OwnerForm>();
            ownerForm.ShowDialog();
        }

        private void buttonReview_Click(object sender, EventArgs e)
        {
            var reviewForm = _serviceProvider.GetRequiredService<ReviewForm>();
            reviewForm.ShowDialog();
        }

        private void buttonReviewer_Click(object sender, EventArgs e)
        {
            var reviewerForm = _serviceProvider.GetRequiredService<ReviewerForm>();
            reviewerForm.ShowDialog();
        }

        private void buttonPokemon_Click(object sender, EventArgs e)
        {
            var pokemonForm = _serviceProvider.GetRequiredService<PokemonForm>();
            pokemonForm.ShowDialog();
        }

        private void buttonPokemonFood_Click(object sender, EventArgs e)
        {
            var pokemonFoodForm = _serviceProvider.GetRequiredService<PokemonFoodForm>();
            pokemonFoodForm.ShowDialog();
        }
    }
}