using PokemonConsoleApp.InputDtos;
using PokemonConsoleApp.Models;
using System.Net.Http.Json;

namespace PokemonConsoleApp
{
    public class ReviewerMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public ReviewerMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetAllReviewers()
        {
            Console.Clear();
            Console.WriteLine("//Reviewer List is Loading\n");

            try
            {
                //apiye istek atıyoruz
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Reviewer");

                if (response.IsSuccessStatusCode)
                {
                    //json dosyasını dönüştürüyoruz
                    List<Reviewer> reviewerList = await response.Content.ReadFromJsonAsync<List<Reviewer>>();

                    Console.WriteLine("--- REVIEWER LİST ---");

                    //foreach ile döndürüyoruz
                    if (reviewerList != null && reviewerList.Count > 0)
                    {
                        foreach (Reviewer r in reviewerList)
                        {
                            Console.WriteLine($"{r.Id} - First Name: {r.FirstName} Last Name: {r.LastName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no reviewers in the database.");
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


        public async Task GetReviewerById()
        {
            Console.Clear();
            Console.WriteLine("// Get Reviewer By Id\n");

            Console.Write("ID of the Reviewer you want to find: ");
            string input = Console.ReadLine();

            try
            {
                //harf hatası
                int reviewerId = Convert.ToInt32(input);

                //api kapalıysa gidecek hata mesajı
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Reviewer/{reviewerId}");

                if (response.IsSuccessStatusCode)
                {
                    Reviewer r = await response.Content.ReadFromJsonAsync<Reviewer>();

                    Console.WriteLine("\n--- REVIEWER DETAILS ---");
                    Console.WriteLine($"ID: {r.Id}");
                    Console.WriteLine($"First Name: {r.FirstName}");
                    Console.WriteLine($"Last Name: {r.LastName}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //404 notfound
                    Console.WriteLine($"Reviewer with ID {reviewerId} could not be found.");
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

        
        public async Task CreateReviewer()
        {
            Console.Clear();
            Console.WriteLine("// Create a New Reviewer\n");

            try
            {
                Console.Write("Please enter the first name of the reviewer: ");
                string firstName = Console.ReadLine();

                Console.Write("Please enter the last name of the reviewer: ");
                string lastName = Console.ReadLine();

                ReviewerInputDto newReviewer = new ReviewerInputDto
                {
                    FirstName = firstName,
                    LastName = lastName,
                };

                HttpResponseMessage response = await client.PostAsJsonAsync(baseUrl + "Reviewer", newReviewer);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Reviewer created successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not create the reviewer. Status Code: {response.StatusCode}");

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



        public async Task UpdateReviewer()
        {
            Console.Clear();
            Console.WriteLine("// Update a Reviewer\n");

            Console.Write("Please enter the id of the reviewer: ");
            string input = Console.ReadLine();


            try
            {
                int reviewerId = Convert.ToInt32(input);

                Console.Write("Please enter the first name of the reviewer: ");
                string firstName = Console.ReadLine();

                Console.Write("Please enter the last name of the reviewer: ");
                string lastName = Console.ReadLine();

                ReviewerInputDto updatedReviewer = new ReviewerInputDto
                {
                    FirstName = firstName,
                    LastName = lastName,
                };

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + $"Reviewer/{reviewerId}", updatedReviewer);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Reviewer updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not update the reviewer. Status Code: {response.StatusCode}");

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

        public async Task DeleteReviewer()
        {
            Console.Clear();
            Console.WriteLine("// Delete a Reviewer\n");

            try
            {
                Console.Write("Enter the ID of the reviewer you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int reviewerId))
                {
                    Console.WriteLine("\nInvalid ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Reviewer/{reviewerId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Reviewer deleted successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Reviewer with {reviewerId} ID could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not delete the reviewer. Status Code: {response.StatusCode}");
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







    }
}