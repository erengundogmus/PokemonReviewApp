using PokemonConsoleApp.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace PokemonConsoleApp.Menus
{
    public class FoodAddDropMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public FoodAddDropMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetFoodsByPokemon()
        {
            Console.Clear();
            Console.WriteLine("// Get Foods By Pokemon\n");

            Console.Write("Enter the Pokemon ID: ");
            string input = Console.ReadLine();

            try
            {
                int pokemonId = Convert.ToInt32(input);
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"PokemonFood/pokemon/{pokemonId}");

                if (response.IsSuccessStatusCode)
                {
                    List<Food> foods = await response.Content.ReadFromJsonAsync<List<Food>>();
                    Console.WriteLine($"\n--- FOODS (Pokemon ID: {pokemonId}) ---");

                    if (foods != null && foods.Count > 0)
                    {
                        foreach (Food f in foods)
                        {
                            Console.WriteLine($"{f.Id} - Name: {f.Name} - HP: {f.Hp}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No foods found for this Pokemon.");
                    }
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nPokemon with ID {pokemonId} could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Received an unsuccessful response from API. Status Code: {response.StatusCode}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid Input: Please enter a valid number.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nConnection Error: Could not reach the API.");
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }



        public async Task AddFoodToPokemon()
        {
            Console.Clear();
            Console.WriteLine("// Add Food To Pokemon\n");

            Console.Write("Enter the Food ID: ");
            string foodInput = Console.ReadLine();

            Console.Write("Enter the Pokemon ID: ");
            string pokeInput = Console.ReadLine();

            try
            {
                int foodId = Convert.ToInt32(foodInput);
                int pokemonId = Convert.ToInt32(pokeInput);

                HttpResponseMessage response = await client.PostAsync(baseUrl + $"PokemonFood/{foodId}/pokemon/{pokemonId}", null);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Food added to Pokemon successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nFood or Pokemon could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not add food. Status Code: {response.StatusCode}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid Input: Please enter a valid number.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nConnection Error: Could not reach the API.");
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }



        public async Task RemoveFoodFromPokemon()
        {
            Console.Clear();
            Console.WriteLine("// Remove Food From Pokemon\n");

            Console.Write("Enter the Food ID: ");
            string foodInput = Console.ReadLine();

            Console.Write("Enter the Pokemon ID: ");
            string pokeInput = Console.ReadLine();

            try
            {
                int foodId = Convert.ToInt32(foodInput);
                int pokemonId = Convert.ToInt32(pokeInput);

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"PokemonFood/{foodId}/pokemon/{pokemonId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Food removed from Pokemon successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nFood or Pokemon could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not remove food. Status Code: {response.StatusCode}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid Input: Please enter a valid number.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nConnection Error: Could not reach the API.");
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }




    }
}
