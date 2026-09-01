using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokemonWinFormsApp.Category;
using PokemonWinFormsApp.Country;
using PokemonWinFormsApp.Food;
using PokemonWinFormsApp.Owner;
using PokemonWinFormsApp.Pokemon;
using PokemonWinFormsApp.PokemonFood;
using PokemonWinFormsApp.Review;
using PokemonWinFormsApp.Reviewer;

namespace PokemonWinFormsApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string apiBaseUrl = "https://localhost:7013/api/";

                    services.AddHttpClient("ApiClient", client =>
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                    });

                    services.AddHttpClient<IAuthService, AuthService>(client =>
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                    });

                    services.AddTransient(typeof(IGenericApiService<,>), typeof(GenericApiService<,>));

                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();

                    // Category Forms
                    services.AddTransient<CategoryForm>();
                    services.AddTransient<CategoryCreateForm>();
                    services.AddTransient<CategoryDetailForm>();
                    services.AddTransient<CategoryUpdateForm>();

                    // Country Forms
                    services.AddTransient<CountryForm>();
                    services.AddTransient<CountryCreateForm>();
                    services.AddTransient<CountryDetailForm>();
                    services.AddTransient<CountryUpdateForm>();

                    // Food Forms
                    services.AddTransient<FoodForm>();
                    services.AddTransient<FoodCreateForm>();
                    services.AddTransient<FoodDetailForm>();
                    services.AddTransient<FoodUpdateForm>();

                    // Owner Forms
                    services.AddTransient<OwnerForm>();
                    services.AddTransient<OwnerCreateForm>();
                    services.AddTransient<OwnerDetailForm>();
                    services.AddTransient<OwnerUpdateForm>();

                    // Review Forms
                    services.AddTransient<ReviewForm>();
                    services.AddTransient<ReviewCreateForm>();
                    services.AddTransient<ReviewDetailForm>();
                    services.AddTransient<ReviewUpdateForm>();

                    // Reviewer Forms
                    services.AddTransient<ReviewerForm>();
                    services.AddTransient<ReviewerCreateForm>();
                    services.AddTransient<ReviewerDetailForm>();
                    services.AddTransient<ReviewerUpdateForm>();

                    // Pokemon Forms
                    services.AddTransient<PokemonForm>();
                    services.AddTransient<PokemonCreateForm>();
                    services.AddTransient<PokemonDetailForm>();
                    services.AddTransient<PokemonUpdateForm>();

                    // PokemonFood Form
                    services.AddTransient<PokemonFoodForm>();
                })
                .Build();

            var loginForm = host.Services.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }
    }
}