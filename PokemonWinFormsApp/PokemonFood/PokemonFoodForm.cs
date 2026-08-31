using Microsoft.Extensions.DependencyInjection;
using PokemonReviewApp.Dto;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonWinFormsApp.PokemonFood
{
    public partial class PokemonFoodForm : Form
    {
        private readonly IGenericApiService<PokemonInputDto, PokemonOutputDto> _pokemonService;
        private readonly IGenericApiService<FoodInputDto, FoodOutputDto> _foodService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;

        // Sabit URL yerine HttpClient'ın BaseAddress'ine uygun relative path kullanıyoruz
        private readonly string pokemonFoodApiUrl = "pokemonfood";

        public PokemonFoodForm(
            IGenericApiService<PokemonInputDto, PokemonOutputDto> pokemonService,
            IGenericApiService<FoodInputDto, FoodOutputDto> foodService,
            IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            _pokemonService = pokemonService;
            _foodService = foodService;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("ApiClient");
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
                var pokemons = await _pokemonService.GetAllAsync("pokemon");
                if (pokemons != null)
                {
                    dataGridView1.DataSource = pokemons.ToList();
                }

                var foods = await _foodService.GetAllAsync("food");
                if (foods != null)
                {
                    dataGridView2.DataSource = foods.ToList();
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
                        var menuFoods = await _client.GetFromJsonAsync<List<FoodOutputDto>>($"{pokemonFoodApiUrl}/pokemon/{selectedPokemon.Id}");
                        if (menuFoods != null)
                        {
                            dataGridView3.DataSource = menuFoods.ToList();
                        }
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
            if (dataGridView1.CurrentRow != null && dataGridView2.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                var selectedFood = dataGridView2.CurrentRow.DataBoundItem as FoodOutputDto;

                if (selectedPokemon != null && selectedFood != null)
                {
                    try
                    {
                        HttpResponseMessage response = await _client.PostAsync($"{pokemonFoodApiUrl}/{selectedFood.Id}/pokemon/{selectedPokemon.Id}", null);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Food successfully added to pokemon's menu!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dataGridView1.CurrentRow != null && dataGridView3.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                var selectedFood = dataGridView3.CurrentRow.DataBoundItem as FoodOutputDto;

                if (selectedPokemon != null && selectedFood != null)
                {
                    try
                    {
                        HttpResponseMessage response = await _client.DeleteAsync($"{pokemonFoodApiUrl}/{selectedFood.Id}/pokemon/{selectedPokemon.Id}");

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