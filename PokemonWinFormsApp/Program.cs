using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

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

                    services.AddTransient<IApiService, ApiService>();

                    //projedeki tüm form sınıflarını bulup otomatik olarak transient kaydetme
                    var formTypes = Assembly.GetExecutingAssembly().GetTypes()
                                            .Where(t => t.IsSubclassOf(typeof(Form)));

                    foreach (var formType in formTypes)
                    {
                        services.AddTransient(formType);
                    }
                })
                .Build();

            var loginForm = host.Services.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }
    }
}