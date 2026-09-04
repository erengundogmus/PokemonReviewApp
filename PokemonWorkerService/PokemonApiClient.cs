using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PokemonWorkerService;

public class PokemonApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PokemonApiClient> _logger;
    private string? _token;

    public PokemonApiClient(HttpClient httpClient, IConfiguration configuration, ILogger<PokemonApiClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    private async Task AuthenticateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker Service API icin kimlik dogrulamasi yapiyor...");

        var username = _configuration["PokemonApiSettings:ServiceAccount:Username"];
        var password = _configuration["PokemonApiSettings:ServiceAccount:Password"];

        var loginData = new { Username = username, Password = password };

        var response = await _httpClient.PostAsJsonAsync("user/login", loginData, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancellationToken);
            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                _token = result.Token;
                _logger.LogInformation("Kimlik dogrulama basarili, token alindi.");
            }
        }
        else
        {
            _logger.LogError("Kimlik dogrulama basarisiz oldu. HTTP Status: {StatusCode}", response.StatusCode);
            throw new UnauthorizedAccessException("API'ye giris yapilamadi. appsettings.json icerisindeki kullanici bilgilerini kontrol edin.");
        }
    }

    public async Task<List<PokemonItem>?> GetPokemonsAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_token))
        {
            await AuthenticateAsync(cancellationToken);
        }
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _httpClient.GetAsync("pokemon", cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning("Token suresi dolmus (401). Yeniden token aliniyor...");
            await AuthenticateAsync(cancellationToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            response = await _httpClient.GetAsync("pokemon", cancellationToken);
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<PokemonItem>>(cancellationToken: cancellationToken);
    }
}

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
}

public class PokemonItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }

    [JsonPropertyName("ownerId")]
    public int OwnerId { get; set; }

    [JsonPropertyName("ownerName")]
    public string OwnerName { get; set; } = string.Empty;

    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; } = string.Empty;
}