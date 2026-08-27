using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PokemonWorkerService;

public class PokemonApiClient
{
    private readonly HttpClient _httpClient;

    public PokemonApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // BaseAddress atamasını buradan kaldırdık, artık Program.cs hallediyor.
    }

    public async Task<List<PokemonItem>?> GetPokemonsAsync(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<List<PokemonItem>>("pokemon", cancellationToken);
    }
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