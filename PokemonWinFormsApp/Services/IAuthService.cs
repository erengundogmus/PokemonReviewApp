using PokemonWinFormsApp;
using System.Net.Http.Json;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(string endpoint, object loginDto);
}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponseDto?> LoginAsync(string endpoint, object loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, loginDto);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
    }
}