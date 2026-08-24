using PokemonConsoleApp.InputDtos;
using PokemonConsoleApp.Models;
using System.Net.Http.Json;

namespace PokemonConsoleApp
{
    public class ReviewMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public ReviewMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetAllReviews()
        {
            Console.Clear();
            Console.WriteLine("//Review List is Loading\n");

            try
            {
                //apiye istek atıyoruz
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Review");

                if (response.IsSuccessStatusCode)
                {
                    //json dosyasını element listesi olarak okuyoruz (Rating hatasını aşmak için)
                    var reviewList = await response.Content.ReadFromJsonAsync<List<System.Text.Json.JsonElement>>();

                    Console.WriteLine("--- REVIEW LİST ---");

                    //foreach ile döndürüyoruz
                    if (reviewList != null && reviewList.Count > 0)
                    {
                        foreach (var r in reviewList)
                        {
                            int id = r.GetProperty("id").GetInt32();
                            string title = r.GetProperty("title").GetString();
                            string text = r.GetProperty("text").GetString();

                            //rating'i ondalıklı alıp int'e çeviriyoruz
                            int rating = (int)r.GetProperty("rating").GetDecimal();

                            Console.WriteLine($"{id} - Title: {title} Text: {text} Rating: {rating}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no reviews in the database.");
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


        public async Task GetReviewById()
        {
            Console.Clear();
            Console.WriteLine("// Get Review By Id\n");

            Console.Write("ID of the Review you want to find: ");
            string input = Console.ReadLine();

            try
            {
                //harf hatası
                int reviewId = Convert.ToInt32(input);

                //api kapalıysa gidecek hata mesajı
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Review/{reviewId}");

                if (response.IsSuccessStatusCode)
                {
                    // json dosyasını element olarak okuyoruz
                    var r = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();

                    int id = r.GetProperty("id").GetInt32();
                    string title = r.GetProperty("title").GetString();
                    string text = r.GetProperty("text").GetString();
                    int rating = (int)r.GetProperty("rating").GetDecimal();

                    Console.WriteLine("\n--- REVIEW DETAILS ---");
                    Console.WriteLine($"ID: {id}");
                    Console.WriteLine($"Title: {title}");
                    Console.WriteLine($"Text: {text}");
                    Console.WriteLine($"Rating: {rating}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //404 notfound
                    Console.WriteLine($"Review with ID {reviewId} could not be found.");
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


        public async Task CreateReview()
        {
            Console.Clear();
            Console.WriteLine("// Create a New Review\n");

            try
            {
                Console.Write("Please enter title of the review: ");
                string title = Console.ReadLine();

                Console.Write("Please enter the text of review: ");
                string text = Console.ReadLine();
                
                Console.Write("Please enter the rating of review: ");
                int rating = Convert.ToInt32(Console.ReadLine());
                
                Console.Write("Please enter the reviewer Id: ");
                int reviewerId = Convert.ToInt32(Console.ReadLine());
                
                Console.Write("Please enter the pokemon Id: ");
                int pokemonId = Convert.ToInt32(Console.ReadLine());


                ReviewInputDto newReview = new ReviewInputDto
                {
                    Title = title,
                    Text = text,
                    Rating = rating,
                    ReviewerId = reviewerId,
                    PokemonId = pokemonId
                };

                HttpResponseMessage response = await client.PostAsJsonAsync(baseUrl + "Review", newReview);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Review created successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not create the review. Status Code: {response.StatusCode}");

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



        public async Task UpdateReview()
        {
            Console.Clear();
            Console.WriteLine("// Update a Review\n");

            Console.Write("Please enter the id of the review: ");
            string input = Console.ReadLine();


            try
            {
                int reviewId = Convert.ToInt32(input);

                Console.Write("Please enter title of the review: ");
                string title = Console.ReadLine();

                Console.Write("Please enter the text of review: ");
                string text = Console.ReadLine();

                Console.Write("Please enter the rating of review: ");
                int rating = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please enter the reviewer Id: ");
                int reviewerId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please enter the pokemon Id: ");
                int pokemonId = Convert.ToInt32(Console.ReadLine());


                ReviewInputDto updatedReview = new ReviewInputDto
                {
                    Title = title,
                    Text = text,
                    Rating = rating,
                    ReviewerId = reviewerId,
                    PokemonId = pokemonId
                };

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + $"Review/{reviewId}", updatedReview);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Review updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not update the review. Status Code: {response.StatusCode}");

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

        public async Task DeleteReview()
        {
            Console.Clear();
            Console.WriteLine("// Delete a Review\n");

            try
            {
                Console.Write("Enter the ID of the review you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int reviewId))
                {
                    Console.WriteLine("\nInvalid ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Review/{reviewId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Review deleted successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nReview with {reviewId} ID could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not delete the review. Status Code: {response.StatusCode}");
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





        public async Task GetReviewsForAPokemon()
        {
            Console.Clear();
            Console.WriteLine("// Get Reviews For A Pokemon\n");

            Console.Write("Enter the Pokemon ID: ");
            string input = Console.ReadLine();

            try
            {
                int pokeId = Convert.ToInt32(input);
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Review/pokemon/{pokeId}");

                if (response.IsSuccessStatusCode)
                {
                    var reviews = await response.Content.ReadFromJsonAsync<List<System.Text.Json.JsonElement>>();

                    Console.WriteLine($"\n--- REVIEWS (Pokemon ID: {pokeId}) ---");

                    if (reviews != null && reviews.Count > 0)
                    {
                        foreach (var r in reviews)
                        {
                            int id = r.GetProperty("id").GetInt32();
                            string title = r.GetProperty("title").GetString();
                            string text = r.GetProperty("text").GetString();
                            string pokemonName = r.GetProperty("pokemonName").GetString();
                            int rating = (int)r.GetProperty("rating").GetDecimal();

                            Console.WriteLine($"{id} - Pokemon: {pokemonName} - Title: {title} - Rating: {rating} - Text: {text}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No reviews found for this Pokemon.");
                    }
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nPokemon with ID {pokeId} could not be found.");
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