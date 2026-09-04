using PokemonWinFormsApp;
using System.Net;
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

    private void CheckForbidden(HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Forbidden || statusCode == HttpStatusCode.Unauthorized)
        {
            MessageBox.Show("You do not have permission for this action or your session is invalid.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(string endpoint)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync(endpoint);

        CheckForbidden(response.StatusCode);

        if (!response.IsSuccessStatusCode)
            return Enumerable.Empty<T>();

        return await response.Content.ReadFromJsonAsync<IEnumerable<T>>() ?? Enumerable.Empty<T>();
    }

    public async Task<T?> GetByIdAsync<T>(string endpoint, int id)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync($"{endpoint}/{id}");

        CheckForbidden(response.StatusCode);

        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<bool> CreateAsync<T>(string endpoint, T data)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync(endpoint, data);

        if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
        {
            MessageBox.Show("Server rejected this action (Unauthorized Access).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync<T>(string endpoint, int id, T data)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PutAsJsonAsync($"{endpoint}/{id}", data);

        if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
        {
            MessageBox.Show("Server rejected this action (Unauthorized Access).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string endpoint, int id)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");

        if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
        {
            MessageBox.Show("Server rejected this action (Unauthorized Access).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return response.IsSuccessStatusCode;
    }
}