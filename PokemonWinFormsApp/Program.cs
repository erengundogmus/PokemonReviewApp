using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace PokemonWinFormsApp
{
    internal static class Program
    {
        public static ILifetimeScope Container { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                //servis sağlayıcı fabrikasını Autofac olarak seçiyoruz
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())

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
                })

                //autofac container'a formları kaydediyoruz sürekli yeni form kaydetmemek ve diğer türleri de autoface eklemesin diye filtreledik
                .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    var assembly = Assembly.GetExecutingAssembly();

                    var types = assembly.GetTypes()
                        .Where(t => t.IsClass
                                    && !t.IsAbstract
                                    && !t.IsInterface
                                    && !t.IsGenericTypeDefinition
                                    && !typeof(UserControl).IsAssignableFrom(t)
                                    && typeof(Form).IsAssignableFrom(t));

                    foreach (var type in types)
                    {
                        containerBuilder
                            .RegisterType(type)
                            .InstancePerDependency() //nesne her istekte yeniden üretilir
                            .ExternallyOwned(); //formun kapatılma kontrolü winforms'ta
                    }
                })
                .Build();

            //autofac container'ını merkezi olarak saklıyoruz
            Container = host.Services.GetRequiredService<ILifetimeScope>();

            //ilk formu Autofac oluşturuyor
            var loginForm = Container.Resolve<LoginForm>();

            Application.Run(loginForm);
        }
    }
}
