using System.Net.Http.Json;

public class GenericApiService<TInput, TOutput> : IGenericApiService<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    private readonly HttpClient _httpClient;

    public GenericApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<TOutput>> GetAllAsync(string endpoint)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TOutput>>(endpoint)
               ?? Enumerable.Empty<TOutput>();
    }

    public async Task<TOutput?> GetByIdAsync(string endpoint, int id)
    {
        return await _httpClient.GetFromJsonAsync<TOutput>($"{endpoint}/{id}");
    }

    public async Task<bool> CreateAsync(string endpoint, TInput dto)
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(string endpoint, int id, TInput dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{endpoint}/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string endpoint, int id)
    {
        var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
        return response.IsSuccessStatusCode;
    }
}