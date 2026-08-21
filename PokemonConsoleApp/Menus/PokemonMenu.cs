using PokemonConsoleApp.InputDtos;
using PokemonConsoleApp.Models;
using System.Net.Http.Json;

namespace PokemonConsoleApp
{
    public class PokemonMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public PokemonMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetAllPokemons()
        {
            Console.Clear();
            Console.WriteLine("//Pokemon List is Loading\n");

            try
            {
                //apiye istek atıyoruz
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Pokemon");

                if (response.IsSuccessStatusCode)
                {
                    //json dosyasını dönüştürüyoruz
                    List<Pokemon> pokemonList = await response.Content.ReadFromJsonAsync<List<Pokemon>>();

                    Console.WriteLine("--- POKEMON LİST ---");

                    //foreach ile döndürüyoruz
                    if (pokemonList != null && pokemonList.Count > 0)
                    {
                        foreach (Pokemon p in pokemonList)
                        {
                            Console.WriteLine($"{p.Id} - Name: {p.Name} | Birth Date: {p.BirthDate.ToString("yyyy-MM-dd")}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no pokemons in the database.");
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

        public async Task GetPokemonById()
        {
            Console.Clear();
            Console.WriteLine("// Get Pokemon By Id\n");

            Console.Write("ID of the Pokemon you want to find: ");
            string input = Console.ReadLine();

            try
            {
                //harf hatası
                int pokemonId = Convert.ToInt32(input);

                //api kapalıysa gidecek hata mesajı
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Pokemon/{pokemonId}");

                if (response.IsSuccessStatusCode)
                {
                    Pokemon p = await response.Content.ReadFromJsonAsync<Pokemon>();

                    Console.WriteLine("\n--- POKEMON DETAILS ---");
                    Console.WriteLine($"ID: {p.Id}");
                    Console.WriteLine($"Name: {p.Name}");
                    Console.WriteLine($"Birth Date: {p.BirthDate.ToString("yyyy-MM-dd")}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //404 notfound
                    Console.WriteLine($"Pokemon with ID {pokemonId} could not be found.");
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


        public async Task CreatePokemon()
        {
            Console.Clear();
            Console.WriteLine("// Create a New Pokemon\n");

            try
            {
                Console.Write("Please enter the name of the pokemon: ");
                string name = Console.ReadLine();

                Console.Write("Please enter the Birth Date (yyyy-MM-dd): ");
                string dateInput = Console.ReadLine();
                DateTime birthDate = Convert.ToDateTime(dateInput);

                Console.Write("Please enter the Owner ID: ");
                int ownerId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please enter the Category ID: ");
                int categoryId = Convert.ToInt32(Console.ReadLine());

                PokemonInputDto newPokemon = new PokemonInputDto
                {
                    Name = name,
                    BirthDate = birthDate,
                    OwnerId = ownerId,
                    CategoryId = categoryId
                };

                HttpResponseMessage response = await client.PostAsJsonAsync(baseUrl + "Pokemon", newPokemon);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Pokemon created successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not create the pokemon. Status Code: {response.StatusCode}");

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



        public async Task UpdatePokemon()
        {
            Console.Clear();
            Console.WriteLine("// Update a Pokemon\n");

            Console.Write("Please enter the id of the pokemon: ");
            string input = Console.ReadLine();


            try
            {
                int pokemonId = Convert.ToInt32(input);

                Console.Write("Please enter the new name of the pokemon: ");
                string name = Console.ReadLine();

                Console.Write("Please enter the new Birth Date (yyyy-MM-dd): ");
                string dateInput = Console.ReadLine();
                DateTime birthDate = Convert.ToDateTime(dateInput);

                Console.Write("Please enter the new Owner ID: ");
                int ownerId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please enter the new Category ID: ");
                int categoryId = Convert.ToInt32(Console.ReadLine());

                PokemonInputDto updatedPokemon = new PokemonInputDto
                {
                    Name = name,
                    BirthDate = birthDate,
                    OwnerId = ownerId,
                    CategoryId = categoryId
                };

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + $"Pokemon/{pokemonId}", updatedPokemon);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Pokemon updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not update the pokemon. Status Code: {response.StatusCode}");

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

        public async Task DeletePokemon()
        {
            Console.Clear();
            Console.WriteLine("// Delete a Pokemon\n");

            try
            {
                Console.Write("Enter the ID of the Pokemon you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int pokemonId))
                {
                    Console.WriteLine("\nInvalid ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Pokemon/{pokemonId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Pokemon deleted successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nPokemon with {pokemonId} ID could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not delete the pokemon. Status Code: {response.StatusCode}");
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



        public async Task GetPokemonRating()
        {
            Console.Clear();
            Console.WriteLine("// Get Pokemon Rating\n");

            Console.Write("Enter the Pokemon ID: ");
            string pokemon = Console.ReadLine();

            try
            {
                int pokemonId = Convert.ToInt32(pokemon);
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Pokemon/{pokemonId}/rating");

                if (response.IsSuccessStatusCode)
                {
                    decimal rating = await response.Content.ReadFromJsonAsync<decimal>();
                    Console.WriteLine($"\n--- RATING ---");
                    Console.WriteLine($"Pokemon (ID: {pokemonId}) has an average rating of: {rating}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nPokemon with ID {pokemonId} could not be found or has no ratings.");
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




    }
}