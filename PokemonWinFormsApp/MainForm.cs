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
            if (!UserSession.HasPermission("FoodList"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var foodForm = new FoodForm();
            foodForm.ShowDialog();
        }

        private void buttonCountry_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CountryList"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var countryForm = new CountryForm();
            countryForm.ShowDialog();
        }

        private void buttonCategory_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CategoryList"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
        }

        private void buttonOwner_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("OwnerList"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ownerForm = new OwnerForm();
            ownerForm.ShowDialog();
        }

        private void buttonReview_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewList"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reviewForm = new ReviewForm();
            reviewForm.ShowDialog();
        }

        private void buttonReviewer_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("ReviewerList"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reviewerForm = new ReviewerForm();
            reviewerForm.ShowDialog();
        }

        private void buttonPokemon_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("PokemonList"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pokemonForm = new PokemonForm();
            pokemonForm.ShowDialog();
        }

        private void buttonPokemonFood_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("PokemonsMenu"))
            {
                MessageBox.Show("You do not have permission to view this menu.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pokemonFoodForm = new PokemonFoodForm();
            pokemonFoodForm.ShowDialog();
        }
    }
}