using PokemonConsoleApp.InputDtos;
using PokemonConsoleApp.Models;
using System.Net.Http.Json;

namespace PokemonConsoleApp
{
    public class OwnerMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public OwnerMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetAllOwners()
        {
            Console.Clear();
            Console.WriteLine("//Owner List is Loading\n");

            try
            {
                //apiye istek atıyoruz
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Owner");

                if (response.IsSuccessStatusCode)
                {
                    //json dosyasını dönüştürüyoruz
                    List<Owner> ownerList = await response.Content.ReadFromJsonAsync<List<Owner>>();

                    Console.WriteLine("--- OWNER LİST ---");

                    //foreach ile döndürüyoruz
                    if (ownerList != null && ownerList.Count > 0)
                    {
                        foreach (Owner o in ownerList)
                        {
                            Console.WriteLine($"{o.Id} - Name: {o.Name} - Gym: {o.Gym}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no owners in the database.");
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

        public async Task GetOwnerById()
        {
            Console.Clear();
            Console.WriteLine("// Get Owner By Id\n");

            Console.Write("ID of the owner you want to find: ");
            string input = Console.ReadLine();

            try
            {
                int ownerId = Convert.ToInt32(input);

                //api kapalıysa gidecek hata mesajı
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Owner/{ownerId}");

                if (response.IsSuccessStatusCode)
                {
                    Owner o = await response.Content.ReadFromJsonAsync<Owner>();

                    Console.WriteLine("\n--- OWNER DETAILS ---");
                    Console.WriteLine($"ID: {o.Id}");
                    Console.WriteLine($"Name: {o.Name}");
                    Console.WriteLine($"Gym: {o.Gym}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //404 notfound
                    Console.WriteLine($"Owner with ID {ownerId} could not be found.");
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


        public async Task CreateOwner()
        {
            Console.Clear();
            Console.WriteLine("// Create a New Owner\n");

            try
            {
                Console.Write("Please enter the name of the owner: ");
                string name = Console.ReadLine();
                
                Console.Write("Please enter the gym of the owner: ");
                string gym = Console.ReadLine();

                OwnerInputDto newOwner = new OwnerInputDto
                {
                    Name = name,
                    Gym = gym
                };

                HttpResponseMessage response = await client.PostAsJsonAsync(baseUrl + "Owner", newOwner);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Owner created successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not create the owner. Status Code: {response.StatusCode}");

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



        public async Task UpdateOwner()
        {
            Console.Clear();
            Console.WriteLine("// Update a Owner\n");

            Console.Write("Please enter the id of the owner: ");
            string input = Console.ReadLine();


            try
            {
                int ownerId = Convert.ToInt32(input);

                Console.Write("Please enter the name of the owner: ");
                string name = Console.ReadLine();

                Console.Write("Please enter the gym of the owner: ");
                string gym = Console.ReadLine();

                OwnerInputDto updatedOwner = new OwnerInputDto
                {
                    Name = name,
                    Gym = gym
                };

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + $"Owner/{ownerId}", updatedOwner);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Owner updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not update the owner. Status Code: {response.StatusCode}");

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

        public async Task DeleteOwner()
        {
            Console.Clear();
            Console.WriteLine("// Delete a Owner\n");

            try
            {
                Console.Write("Enter the ID of the owner you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int ownerId))
                {
                    Console.WriteLine("\nInvalid ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Owner/{ownerId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Owner deleted successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nOwner with {ownerId} ID could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not delete the owner. Status Code: {response.StatusCode}");
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



        public async Task GetPokemonsByOwner()
        {
            Console.Clear();
            Console.WriteLine("// Get Pokemons By Owner\n");

            Console.Write("Enter the Owner ID: ");
            string input = Console.ReadLine();

            try
            {
                int ownerId = Convert.ToInt32(input);
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Owner/{ownerId}/pokemon");

                if (response.IsSuccessStatusCode)
                {
                    List<Pokemon> pokemons = await response.Content.ReadFromJsonAsync<List<Pokemon>>();
                    Console.WriteLine($"\n--- POKEMONS (Owner ID: {ownerId}) ---");

                    if (pokemons != null && pokemons.Count > 0)
                    {
                        foreach (Pokemon p in pokemons)
                        {
                            Console.WriteLine($"{p.Id} - Name: {p.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No pokemons found for this owner.");
                    }
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nOwner with ID {ownerId} could not be found.");
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