using Autofac;
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
            var foodForm = Program.Container.Resolve<FoodForm>();
            foodForm.ShowDialog();
        }

        private void buttonCountry_Click(object sender, EventArgs e)
        {
            var countryForm = Program.Container.Resolve<CountryForm>();
            countryForm.ShowDialog();
        }

        private void buttonCategory_Click(object sender, EventArgs e)
        {
            var categoryForm = Program.Container.Resolve<CategoryForm>();
            categoryForm.ShowDialog();
        }

        private void buttonOwner_Click(object sender, EventArgs e)
        {
            var ownerForm = Program.Container.Resolve<OwnerForm>();
            ownerForm.ShowDialog();
        }

        private void buttonReview_Click(object sender, EventArgs e)
        {
            var reviewForm = Program.Container.Resolve<ReviewForm>();
            reviewForm.ShowDialog();
        }

        private void buttonReviewer_Click(object sender, EventArgs e)
        {
            var reviewerForm = Program.Container.Resolve<ReviewerForm>();
            reviewerForm.ShowDialog();
        }

        private void buttonPokemon_Click(object sender, EventArgs e)
        {
            var pokemonForm = Program.Container.Resolve<PokemonForm>();
            pokemonForm.ShowDialog();
        }

        private void buttonPokemonFood_Click(object sender, EventArgs e)
        {
            var pokemonFoodForm = Program.Container.Resolve<PokemonFoodForm>();
            pokemonFoodForm.ShowDialog();
        }
    }
}