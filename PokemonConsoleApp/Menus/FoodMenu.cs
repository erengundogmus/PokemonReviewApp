using PokemonConsoleApp.InputDtos;
using PokemonConsoleApp.Models;
using System.Net.Http.Json;

namespace PokemonConsoleApp
{
    public class FoodMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public FoodMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetAllFoods()
        {
            Console.Clear();
            Console.WriteLine("//Food List is Loading\n");

            try
            {
                //apiye istek atıyoruz
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Food");

                if (response.IsSuccessStatusCode)
                {
                    //json dosyasını dönüştürüyoruz
                    List<Food> foodList = await response.Content.ReadFromJsonAsync<List<Food>>();

                    Console.WriteLine("--- FOOD LİST ---");

                    //foreach ile döndürüyoruz
                    if (foodList != null && foodList.Count > 0)
                    {
                        foreach (Food o in foodList)
                        {
                            Console.WriteLine($"{o.Id} - Name: {o.Name} - Hp: {o.Hp}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no foods in the database.");
                    }

                    Console.WriteLine("-----------------------");
                }
                else
                {
                    Console.WriteLine($"Error: Received an unsuccessful response from API. Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Connection Error: Could not reach the API.");
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error has occured: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public async Task GetFoodById()
        {
            Console.Clear();
            Console.WriteLine("// Get Food By Id\n");

            Console.Write("ID of the food you want to find: ");
            string input = Console.ReadLine();

            try
            {
                int foodId = Convert.ToInt32(input);

                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Food/{foodId}");

                if (response.IsSuccessStatusCode)
                {
                    Food f = await response.Content.ReadFromJsonAsync<Food>();

                    Console.WriteLine("\n--- FOOD DETAILS ---");
                    Console.WriteLine($"ID: {f.Id}");
                    Console.WriteLine($"Name: {f.Name}");
                    Console.WriteLine($"Hp: {f.Hp}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //404 notfound
                    Console.WriteLine($"Food with ID {foodId} could not be found.");
                }
                else
                {
                    Console.WriteLine($"Error: Received an unsuccessful response from API. Status Code: {response.StatusCode}");
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


        public async Task CreateFood()
        {
            Console.Clear();
            Console.WriteLine("// Create a New Food\n");

            try
            {
                Console.Write("Please enter the name of the food: ");
                string name = Console.ReadLine();

                Console.Write("Please enter the hp of the food: ");
                int hp = Convert.ToInt32(Console.ReadLine());

                FoodInputDto newFood = new FoodInputDto
                {
                    Name = name,
                    Hp = hp
                };

                HttpResponseMessage response = await client.PostAsJsonAsync(baseUrl + "Food", newFood);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Food created successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not create the food. Status Code: {response.StatusCode}");

                    string errorDetail = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Details: {errorDetail}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid Format: Please ensure dates and numbers are correct.");
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



        public async Task UpdateFood()
        {
            Console.Clear();
            Console.WriteLine("// Update a Food\n");

            Console.Write("Please enter the id of the food: ");
            string input = Console.ReadLine();


            try
            {
                int foodId = Convert.ToInt32(input);

                Console.Write("Please enter the name of the food: ");
                string name = Console.ReadLine();

                Console.Write("Please enter the hp of the food: ");
                int hp = Convert.ToInt32(Console.ReadLine());

                FoodInputDto updatedFood = new FoodInputDto
                {
                    Name = name,
                    Hp = hp
                };

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + $"Food/{foodId}", updatedFood);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Food updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not update the food. Status Code: {response.StatusCode}");

                    string errorDetail = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Details: {errorDetail}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid Format: Please ensure dates and numbers are correct.");
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

        public async Task DeleteFood()
        {
            Console.Clear();
            Console.WriteLine("// Delete a Food\n");

            try
            {
                Console.Write("Enter the ID of the food you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int foodId))
                {
                    Console.WriteLine("\nInvalid ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Food/{foodId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Food deleted successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nFood with {foodId} ID could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not delete the food. Status Code: {response.StatusCode}");
                }
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



        public async Task GetFoodsByPokemon()
        {
            Console.Clear();
            Console.WriteLine("// Get Foods By Pokemon\n");

            Console.Write("Enter the Pokemon ID: ");
            string input = Console.ReadLine();

            try
            {
                int pokemonId = Convert.ToInt32(input);
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Food/pokemon/{pokemonId}");

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

                HttpResponseMessage response = await client.PostAsync(baseUrl + $"Food/{foodId}/pokemon/{pokemonId}", null);

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

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Food/{foodId}/pokemon/{pokemonId}");

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