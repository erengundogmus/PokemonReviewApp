using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.PokemonFood
{
    public partial class PokemonFoodForm : Form
    {
        private readonly string pokemonApiUrl = "https://localhost:7013/api/pokemon";
        private readonly string foodApiUrl = "https://localhost:7013/api/food";
        private readonly string pokemonFoodApiUrl = "https://localhost:7013/api/pokemonfood";
        private readonly HttpClient client = new HttpClient();

        public PokemonFoodForm()
        {
            InitializeComponent();
        }

        private async void PokemonFoodForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var pokemons = await client.GetFromJsonAsync<List<PokemonOutputDto>>(pokemonApiUrl);
                if (pokemons != null)
                {
                    dataGridView1.DataSource = pokemons;
                }

                var foods = await client.GetFromJsonAsync<List<FoodOutputDto>>(foodApiUrl);
                if (foods != null)
                {
                    dataGridView2.DataSource = foods;
                }

                dataGridView3.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonPokemonsMenu_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                if (selectedPokemon != null)
                {
                    try
                    {
                        //3. grid'e pokemonun menüsünü getirmek için
                        var menuFoods = await client.GetFromJsonAsync<List<FoodOutputDto>>($"{pokemonFoodApiUrl}/pokemon/{selectedPokemon.Id}");
                        dataGridView3.DataSource = menuFoods;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading pokemon's menu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pokemon from the first list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonAddToMenu_Click(object sender, EventArgs e)
        {
            //pokemon ve eklenecek yemeği seçmek için
            if (dataGridView1.CurrentRow != null && dataGridView2.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                var selectedFood = dataGridView2.CurrentRow.DataBoundItem as FoodOutputDto;

                if (selectedPokemon != null && selectedFood != null)
                {
                    try
                    {
                        HttpResponseMessage response = await client.PostAsync($"{pokemonFoodApiUrl}/{selectedFood.Id}/pokemon/{selectedPokemon.Id}", null);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Food successfully added to pokemon's menu!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //menüyü işlemden sonra tekrar getirmek için
                            buttonPokemonsMenu_Click(sender, e);
                        }
                        else
                        {
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            MessageBox.Show("Failed to add food: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pokemon from the first grid and a food from the second grid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonRemoveFromMenu_Click(object sender, EventArgs e)
        {
            //pokemon ve menüsünden yemek seçmek için
            if (dataGridView1.CurrentRow != null && dataGridView3.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                var selectedFood = dataGridView3.CurrentRow.DataBoundItem as FoodOutputDto;

                if (selectedPokemon != null && selectedFood != null)
                {
                    try
                    {
                        HttpResponseMessage response = await client.DeleteAsync($"{pokemonFoodApiUrl}/{selectedFood.Id}/pokemon/{selectedPokemon.Id}");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Food successfully removed from pokemon's menu!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            buttonPokemonsMenu_Click(sender, e);
                        }
                        else
                        {
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            MessageBox.Show("Failed to remove food: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pokemon from the first grid and a food from the menu (third grid) to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}