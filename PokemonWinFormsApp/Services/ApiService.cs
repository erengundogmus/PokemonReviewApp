using PokemonWinFormsApp;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }
    private void SetAuthorizationHeader()
    {
        if (!string.IsNullOrEmpty(UserSession.Token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserSession.Token);
        }
    }
    public async Task<IEnumerable<T>> GetAllAsync<T>(string endpoint)
    {
        SetAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<IEnumerable<T>>(endpoint) ?? Enumerable.Empty<T>();
    }
    public async Task<T?> GetByIdAsync<T>(string endpoint, int id)
    {
        SetAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<T>($"{endpoint}/{id}");
    }
    public async Task<bool> CreateAsync<T>(string endpoint, T data)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync(endpoint, data);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync<T>(string endpoint, int id, T data)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PutAsJsonAsync($"{endpoint}/{id}", data);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteAsync(string endpoint, int id)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
        return response.IsSuccessStatusCode;
    }
}