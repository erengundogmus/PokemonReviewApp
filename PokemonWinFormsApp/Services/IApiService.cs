public interface IApiService
{
    Task<IEnumerable<T>> GetAllAsync<T>(string endpoint);
    Task<T?> GetByIdAsync<T>(string endpoint, int id);

    Task<bool> CreateAsync<T>(string endpoint, T data);
    Task<bool> UpdateAsync<T>(string endpoint, int id, T data);

    Task<bool> DeleteAsync(string endpoint, int id);
}