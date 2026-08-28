using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PokemonWorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly PokemonApiClient _pokemonApiClient;
    private readonly int _intervalInSeconds;

    public Worker(ILogger<Worker> logger, PokemonApiClient pokemonApiClient, IConfiguration configuration)
    {
        _logger = logger;
        _pokemonApiClient = pokemonApiClient;

        //süreyi appsettings.json'dan okuyor(eğer bulamazsa varsayılan olarak 6 saniye yapıyor)
        _intervalInSeconds = configuration.GetValue<int>("PokemonApiSettings:IntervalInSeconds", 6);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //zamanlayıcıyı appsettings'ten gelen süreyle başlatıyor
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_intervalInSeconds));

        await DoWorkAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await DoWorkAsync(stoppingToken);
        }
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Serilog saat bilgisini otomatik eklediği için manuel timestamp'i sildik
            _logger.LogInformation("API'ye istek atildi ve pokemonlar listeleniyor...");

            var pokemons = await _pokemonApiClient.GetPokemonsAsync(cancellationToken);

            if (pokemons != null && pokemons.Any())
            {
                int index = 1;
                foreach (var pokemon in pokemons)
                {
                    // ILogger'ın parametrik loglama (Structured Logging) yapısını kullanıyoruz
                    _logger.LogInformation("  {Index}. ID: {Id,-5} | Ad: {Name,-10} | Kategori: {Category,-8} | Sahip: {Owner} | Tarih: {Date}",
                        index++, pokemon.Id, pokemon.Name, pokemon.CategoryName, pokemon.OwnerName, pokemon.BirthDate.ToString("dd.MM.yyyy HH:mm"));
                }
                _logger.LogInformation(new string('-', 100));
            }
            else
            {
                _logger.LogWarning("API'den bos veri geldi.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "API istegi sirasinda hata olustu.");
        }
    }
}