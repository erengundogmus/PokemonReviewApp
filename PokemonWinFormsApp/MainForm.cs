using PokemonWinFormsApp.Category;
using PokemonWinFormsApp.Country;
using PokemonWinFormsApp.Pokemon;
using PokemonWinFormsApp.PokemonFood;

namespace PokemonWinFormsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void buttonFood_Click(object sender, EventArgs e)
        {
            var foodForm = new FoodForm();
            foodForm.ShowDialog();
        }
        private void buttonCountry_Click(object sender, EventArgs e)
        {
            var countryForm = new CountryForm();
            countryForm.ShowDialog();
        }
        private void buttonCategory_Click(object sender, EventArgs e)
        {
            var categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
        }
        private void buttonOwner_Click(object sender, EventArgs e)
        {
            var ownerForm = new OwnerForm();
            ownerForm.ShowDialog();
        }
        private void buttonReview_Click(object sender, EventArgs e)
        {
            var reviewForm = new ReviewForm();
            reviewForm.ShowDialog();
        }
        private void buttonReviewer_Click(object sender, EventArgs e)
        {
            var reviewerForm = new ReviewerForm();
            reviewerForm.ShowDialog();
        }
        private void buttonPokemon_Click(object sender, EventArgs e)
        {
            var pokemonForm = new PokemonForm();
            pokemonForm.ShowDialog();
        }
        private void buttonPokemonFood_Click(object sender, EventArgs e)
        {
            var pokemonFoodForm = new PokemonFoodForm();
            pokemonFoodForm.ShowDialog();
        }
    }
}