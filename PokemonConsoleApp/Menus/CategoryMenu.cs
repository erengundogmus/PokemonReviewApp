using PokemonConsoleApp.InputDtos;
using System.Net.Http.Json;

namespace PokemonConsoleApp
{
    public class CategoryMenu
    {
        private readonly HttpClient client;
        private readonly string baseUrl;

        public CategoryMenu(HttpClient client, string baseUrl)
        {
            this.client = client;
            this.baseUrl = baseUrl;
        }

        public async Task GetAllCategories()
        {
            Console.Clear();
            Console.WriteLine("//Category List is Loading\n");

            try
            {
                //apiye istek atıyoruz
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Category");

                if (response.IsSuccessStatusCode)
                {
                    //json dosyasını dönüştürüyoruz
                    List<Category> categoryList = await response.Content.ReadFromJsonAsync<List<Category>>();

                    Console.WriteLine("--- CATEGORY LİST ---");

                    //foreach ile döndürüyoruz
                    if (categoryList != null && categoryList.Count > 0)
                    {
                        foreach (Category c in categoryList)
                        {
                            Console.WriteLine($"{c.Id} - Name: {c.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no categories in the database.");
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

        

        public async Task GetCategoryById()
        {
            Console.Clear();
            Console.WriteLine("// Get Category By Id\n");

            Console.Write("ID of the Category you want to find: ");
            string input = Console.ReadLine();

            try
            {
                int categoryId = Convert.ToInt32(input);

                //api kapalıysa gidecek hata mesajı
                HttpResponseMessage response = await client.GetAsync(baseUrl + $"Category/{categoryId}");

                if (response.IsSuccessStatusCode)
                {
                    Category c = await response.Content.ReadFromJsonAsync<Category>();

                    Console.WriteLine("\n--- CATEGORY DETAILS ---");
                    Console.WriteLine($"ID: {c.Id}");
                    Console.WriteLine($"Name: {c.Name}");
                    Console.WriteLine("-----------------------");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //404 notfound
                    Console.WriteLine($"Category with ID {categoryId} could not be found.");
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

        
        public async Task CreateCategory()
        {
            Console.Clear();
            Console.WriteLine("// Create a New Category\n");

            try
            {
                Console.Write("Please enter the name of the category: ");
                string name = Console.ReadLine();

                CategoryInputDto newCategory = new CategoryInputDto()
                {
                    Name = name

                };

                HttpResponseMessage response = await client.PostAsJsonAsync(baseUrl + "Category", newCategory);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Category created successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not create the category. Status Code: {response.StatusCode}");

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


        
        public async Task UpdateCategory()
        {
            Console.Clear();
            Console.WriteLine("// Update a Category\n");

            Console.Write("Please enter the id of the category: ");
            string input = Console.ReadLine();


            try
            {
                int categoryId = Convert.ToInt32(input);

                Console.Write("Please enter the new name of the category: ");
                string name = Console.ReadLine();

                CategoryInputDto updatedCategory = new CategoryInputDto
                {
                    Name = name,

                };

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + $"Category/{categoryId}", updatedCategory);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nSuccess: Category updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not update the category. Status Code: {response.StatusCode}");

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

        
        public async Task DeleteCategory()
        {
            Console.Clear();
            Console.WriteLine("// Delete a Category\n");

            try
            {
                Console.Write("Enter the ID of the category you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    Console.WriteLine("\nInvalid ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                HttpResponseMessage response = await client.DeleteAsync(baseUrl + $"Category/{categoryId}");

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nSuccess: Category deleted successfully.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\nCategory with {categoryId} ID could not be found.");
                }
                else
                {
                    Console.WriteLine($"\nError: Could not delete the category. Status Code: {response.StatusCode}");
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