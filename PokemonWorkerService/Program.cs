using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using PokemonWorkerService;

var builder = Host.CreateApplicationBuilder(args);

// appsettings.json'dan BaseUrl'i okuyup HttpClient'a veriyoruz
builder.Services.AddHttpClient<PokemonApiClient>(client =>
{
    var baseUrl = builder.Configuration.GetSection("PokemonApiSettings:BaseUrl").Value;
    if (!string.IsNullOrEmpty(baseUrl))
    {
        client.BaseAddress = new Uri(baseUrl);
    }
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();