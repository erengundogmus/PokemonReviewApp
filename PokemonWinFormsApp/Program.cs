using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using PokemonWinFormsApp.Category; // Kategori formları için
using System;
using System.Windows.Forms;

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

                    // 1. Auth Servisi Kaydı
                    services.AddHttpClient<IAuthService, AuthService>(client =>
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                    });

                    // 2. Kategori için Generic Servis Kaydı
                    services.AddHttpClient<IGenericApiService<CategoryInputDto, CategoryOutputDto>, GenericApiService<CategoryInputDto, CategoryOutputDto>>(client =>
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                    });

                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<CategoryForm>();
                    services.AddTransient<CategoryCreateForm>();
                    services.AddTransient<CategoryDetailForm>();
                    services.AddTransient<CategoryUpdateForm>();

                })
                .Build();

            var loginForm = host.Services.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }
    }
}