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
        //pop up menüyü açar
        private void buttonFood_Click(object sender, EventArgs e)
        {
            FoodForm foodForm = new FoodForm();
            foodForm.ShowDialog();
        }

        private void buttonCountry_Click(object sender, EventArgs e)
        {
            CountryForm countryForm = new CountryForm();
            countryForm.ShowDialog();
        }

        private void buttonCategory_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
        }

        private void buttonOwner_Click(object sender, EventArgs e)
        {
            OwnerForm ownerForm = new OwnerForm();
            ownerForm.ShowDialog();
        }
        private void buttonReview_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm();
            reviewForm.ShowDialog();
        }
        private void buttonReviewer_Click(object sender, EventArgs e)
        {
            ReviewerForm reviewerForm = new ReviewerForm();
            reviewerForm.ShowDialog();
        }
        
        private void buttonPokemon_Click(object sender, EventArgs e)
        {
            PokemonForm pokemonForm = new PokemonForm();
            pokemonForm.ShowDialog();
        }
        
        private void buttonPokemonFood_Click(object sender, EventArgs e)
        {
            PokemonFoodForm pokemonFoodForm = new PokemonFoodForm();
            pokemonFoodForm.ShowDialog();
        }
    }
}
