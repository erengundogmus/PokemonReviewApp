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
        static PokemonMenu pokemonMenu = new PokemonMenu(client, baseUrl);

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
                Console.WriteLine("1 - GetAll");
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
                        await pokemonMenu.GetAllPokemons();
                        break;

                    case "2":
                        await pokemonMenu.GetPokemonById();
                        break;

                    case "3":
                        await pokemonMenu.CreatePokemon();
                        break;
                    
                    case "4":
                        await pokemonMenu.UpdatePokemon();
                        break;
                    
                    case "5":
                        await pokemonMenu.DeletePokemon();
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


       








    }
}