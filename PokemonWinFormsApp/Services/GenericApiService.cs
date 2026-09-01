using PokemonWinFormsApp;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class GenericApiService<TInput, TOutput> : IGenericApiService<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    private readonly HttpClient _httpClient;

    public GenericApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    //her istekten önce tokenı httpclient'a ekleyen yardımcı metod
    private void SetAuthorizationHeader()
    {
        if (!string.IsNullOrEmpty(UserSession.Token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserSession.Token);
        }
    }

    public async Task<IEnumerable<TOutput>> GetAllAsync(string endpoint)
    {
        SetAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<IEnumerable<TOutput>>(endpoint)
               ?? Enumerable.Empty<TOutput>();
    }

    public async Task<TOutput?> GetByIdAsync(string endpoint, int id)
    {
        SetAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<TOutput>($"{endpoint}/{id}");
    }

    public async Task<bool> CreateAsync(string endpoint, TInput dto)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync(endpoint, dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(string endpoint, int id, TInput dto)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PutAsJsonAsync($"{endpoint}/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string endpoint, int id)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
        return response.IsSuccessStatusCode;
    }
}