using PokemonConsoleApp.InputDtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PokemonConsoleApp
{
    class Program
    {
        static readonly string baseUrl = "https://localhost:7013/api/";
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("//Ana Menü");
                Console.WriteLine();
                Console.WriteLine("1 - Category");
                Console.WriteLine("2 - Country");
                Console.WriteLine("3 - Food");
                Console.WriteLine("4 - Owner");
                Console.WriteLine("5 - Pokemon");
                Console.WriteLine("6 - Review");
                Console.WriteLine("7 - Reviewer");
                Console.WriteLine("0 - Exit");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the menu you want to continue:");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    //case "1":
                    //    await CategoryMenu();
                    //    break;
                    
                    //case "2":
                    //    await CountryMenu();
                    //    break;
                   
                    //case "3":
                    //    await FoodMenu();
                    //    break;
                  
                    //case "4":
                    //    await OwnerMenu();
                    //    break;
                  
                    case "5":
                        await PokemonMenu();
                        break;
                    
                    //case "6":
                    //    await ReviewMenu();
                    //    break;
                    
                    //case "7":
                    //    await ReviewerMenu();
                    //    break;
                    
                    case "0":
                        exit = true;
                        Console.WriteLine("See you next time. :)");
                        break;
                    
                    default:
                        Console.WriteLine("Invalid choice, press a key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static async Task PokemonMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Pokemon");
                Console.WriteLine("1 - GetAllP");
                Console.WriteLine("2 - GetById");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("0 - Exit to Main Menu");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the process you want to continue: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetAllPokemons();
                        break;

                    case "2":
                        await GetPokemonById();
                        break;

                    case "3":
                        await CreatePokemon();
                        break;

                    case "0":
                        backToMain = true;
                        break;
                    
                    default:
                        Console.WriteLine("Invalid choice, press a key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }


        static async Task GetAllPokemons()
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

        static async Task GetPokemonById()
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


        static async Task CreatePokemon()
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
                Console.WriteLine("\nInvalid Format: Please ensure dates (yyyy-MM-dd) and IDs (numbers) are correct.");
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