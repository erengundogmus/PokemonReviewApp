using PokemonConsoleApp.InputDtos;
using PokemonConsoleApp.Models;
using System.Net.Http.Json;

namespace PokemonConsoleApp
{
    public class CountryMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public CountryMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetAllCountries()
        {
            Console.Clear();
            Console.WriteLine("//Country List is Loading\n");

            try
            {
                //apiye istek atıyoruz
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Country");

                if (response.IsSuccessStatusCode)
                {
                    //json dosyasını dönüştürüyoruz
                    List<Country> countryList = await response.Content.ReadFromJsonAsync<List<Country>>();

                    Console.WriteLine("--- COUNTRY LİST ---");

                    //foreach ile döndürüyoruz
                    if (countryList != null && countryList.Count > 0)
                    {
                        foreach (Country c in countryList)
                        {
                            Console.WriteLine($"{c.Id} - Name: {c.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no countries in the database.");
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



        public async Task GetCountryById()
        {
            Console.Clear();
            Console.WriteLine("// Get Country By Id\n");

            Console.Write("ID of the Country you want to find: ");
            string input = Console.ReadLine();

            try
            {
                int countryId = Convert.ToInt32(input);

                //api kapalıysa gidecek hata mesajı
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Country/{countryId}");

                if (response.IsSuccessStatusCode)
                {
                    Country c = await response.Content.ReadFromJsonAsync<Country>();

                    Console.WriteLine("\n--- Country DETAILS ---");
                    Console.WriteLine($"ID: {c.Id}");
                    Console.WriteLine($"Name: {c.Name}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //404 notfound
                    Console.WriteLine($"Country with ID {countryId} could not be found.");
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

          
        public async Task CreateCountry()
        {
            Console.Clear();
            Console.WriteLine("// Create a New Country\n");

            try
            {
                Console.Write("Please enter the name of the country: ");
                string name = Console.ReadLine();

                CountryInputDto newCountry = new CountryInputDto()
                {
                    Name = name

                };

                HttpResponseMessage response = await client.PostAsJsonAsync(baseUrl + "Country", newCountry);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Country created successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not create the country. Status Code: {response.StatusCode}");

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


        
        public async Task UpdateCountry()
        {
            Console.Clear();
            Console.WriteLine("// Update a Country\n");

            Console.Write("Please enter the id of the country: ");
            string input = Console.ReadLine();


            try
            {
                int countryId = Convert.ToInt32(input);

                Console.Write("Please enter the new name of the country: ");
                string name = Console.ReadLine();

                CountryInputDto updatedCountry = new CountryInputDto
                {
                    Name = name,

                };

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + $"Country/{countryId}", updatedCountry);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Country updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not update the country. Status Code: {response.StatusCode}");

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

        public async Task DeleteCountry()
        {
            Console.Clear();
            Console.WriteLine("// Delete a Country\n");

            try
            {
                Console.Write("Enter the ID of the country you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int countryId))
                {
                    Console.WriteLine("\nInvalid ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Country/{countryId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Country deleted successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nCountry with {countryId} ID could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not delete the country. Status Code: {response.StatusCode}");
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



        public async Task GetCountryOfAnOwner()
        {
            Console.Clear();
            Console.WriteLine("// Get Country Of An Owner\n");

            Console.Write("Enter the owner ID to see it's country: ");
            string input = Console.ReadLine();

            try
            {
                int ownerId = Convert.ToInt32(input);
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Country/owners/{ownerId}");

                if (response.IsSuccessStatusCode)
                {
                    Country c = await response.Content.ReadFromJsonAsync<Country>();
                    Console.WriteLine($"\n--- COUNTRY DETAILS ---");
                    Console.WriteLine($"ID: {c.Id}");
                    Console.WriteLine($"Name: {c.Name}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nOwner with ID {ownerId} could not be found, or has no country.");
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