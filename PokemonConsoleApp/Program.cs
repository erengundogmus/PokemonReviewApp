using PokemonConsoleApp.InputDtos;
using System;
using System.Buffers.Text;
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
        static CategoryMenu categoryMenu = new CategoryMenu(client, baseUrl);
        static CountryMenu countryMenu = new CountryMenu(client, baseUrl);
        static FoodMenu foodMenu = new FoodMenu(client, baseUrl);
        static OwnerMenu ownerMenu = new OwnerMenu(client, baseUrl);
        static PokemonMenu pokemonMenu = new PokemonMenu(client, baseUrl);
        static ReviewerMenu reviewerMenu = new ReviewerMenu(client, baseUrl);
        static ReviewMenu reviewMenu = new ReviewMenu(client, baseUrl);
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
                Console.WriteLine("6 - Reviewer");
                Console.WriteLine("7 - Review");
                Console.WriteLine("0 - Exit");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the menu you want to continue:");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CategoryMenu();
                        break;
                    
                    case "2":
                        await CountryMenu();
                        break;
                   
                    case "3":
                        await FoodMenu();
                        break;
                  
                    case "4":
                        await OwnerMenu();
                        break;
                  
                    case "5":
                        await PokemonMenu();
                        break;
                    
                    case "6":
                        await ReviewerMenu();
                        break;
                    
                    case "7":
                        await ReviewMenu();
                        break;
                    
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



        static async Task CategoryMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Category");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Get All");
                Console.WriteLine("2 - Get By Id");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6 - Get Pokemons By Category");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.WriteLine("0 - Exit to Main Menu");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the process you want to continue: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await categoryMenu.GetAllCategories();
                        break;

                    case "2":
                        await categoryMenu.GetCategoryById();
                        break;

                    case "3":
                        await categoryMenu.CreateCategory();
                        break;

                    case "4":
                        await categoryMenu.UpdateCategory();
                        break;

                    case "5":
                        await categoryMenu.DeleteCategory();
                        break;
                    
                    case "6":
                        await categoryMenu.GetPokemonsByCategory();
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


        static async Task CountryMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Country");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Get All");
                Console.WriteLine("2 - Get By Id");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6 - Get Country Of An Owner");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.WriteLine("0 - Exit to Main Menu");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the process you want to continue: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await countryMenu.GetAllCountries();
                        break;

                    case "2":
                        await countryMenu.GetCountryById();
                        break;

                    case "3":
                        await countryMenu.CreateCountry();
                        break;

                    case "4":
                        await countryMenu.UpdateCountry();
                        break;

                    case "5":
                        await countryMenu.DeleteCountry();
                        break;

                    case "6":
                        await countryMenu.GetCountryOfAnOwner();
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



        static async Task FoodMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Food");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Get All");
                Console.WriteLine("2 - Get By Id");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6 - Get Foods By Pokemon");
                Console.WriteLine("7 - Add Food To Pokemon");
                Console.WriteLine("8 - Remove Food From Pokemon");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.WriteLine("0 - Exit to Main Menu");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the process you want to continue: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await foodMenu.GetAllFoods();
                        break;

                    case "2":
                        await foodMenu.GetFoodById();
                        break;

                    case "3":
                        await foodMenu.CreateFood();
                        break;

                    case "4":
                        await foodMenu.UpdateFood();
                        break;

                    case "5":
                        await foodMenu.DeleteFood();
                        break;
                    
                    case "6":
                        await foodMenu.GetFoodsByPokemon();
                        break;
                    
                    case "7":
                        await foodMenu.AddFoodToPokemon();
                        break;
                    
                    case "8":
                        await foodMenu.RemoveFoodFromPokemon();
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



        static async Task OwnerMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Owner");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Get All");
                Console.WriteLine("2 - Get By Id");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6 - Get Pokemons By Owner");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.WriteLine("0 - Exit to Main Menu");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the process you want to continue: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ownerMenu.GetAllOwners();
                        break;

                    case "2":
                        await ownerMenu.GetOwnerById();
                        break;

                    case "3":
                        await ownerMenu.CreateOwner();
                        break;

                    case "4":
                        await ownerMenu.UpdateOwner();
                        break;

                    case "5":
                        await ownerMenu.DeleteOwner();
                        break;
                    
                    case "6":
                        await ownerMenu.GetPokemonsByOwner();
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



        static async Task PokemonMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Pokemon");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Get All");
                Console.WriteLine("2 - Get By Id");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6 - Get Pokemon Rating");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
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
                    
                    case "6":
                        await pokemonMenu.GetPokemonRating();
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



        static async Task ReviewerMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Reviewer");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Get All");
                Console.WriteLine("2 - Get By Id");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6 - Get Reviews By A Reviewer");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.WriteLine("0 - Exit to Main Menu");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the process you want to continue: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await reviewerMenu.GetAllReviewers();
                        break;

                    case "2":
                        await reviewerMenu.GetReviewerById();
                        break;

                    case "3":
                        await reviewerMenu.CreateReviewer();
                        break;

                    case "4":
                        await reviewerMenu.UpdateReviewer();
                        break;

                    case "5":
                        await reviewerMenu.DeleteReviewer();
                        break;
                    
                    case "6":
                        await reviewerMenu.GetReviewsByAReviewer();
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



        static async Task ReviewMenu()
        {
            bool backToMain = false;

            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Review");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Get All");
                Console.WriteLine("2 - Get By Id");
                Console.WriteLine("3 - Create");
                Console.WriteLine("4 - Update");
                Console.WriteLine("5 - Delete");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6 - Get Reviews For A Pokemon");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.WriteLine("0 - Exit to Main Menu");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Please choose the process you want to continue: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await reviewMenu.GetAllReviews();
                        break;

                    case "2":
                        await reviewMenu.GetReviewById();
                        break;

                    case "3":
                        await reviewMenu.CreateReview();
                        break;

                    case "4":
                        await reviewMenu.UpdateReview();
                        break;

                    case "5":
                        await reviewMenu.DeleteReview();
                        break;
                    
                    case "6":
                        await reviewMenu.GetReviewsForAPokemon();
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