public interface IGenericApiService<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    Task<IEnumerable<TOutput>> GetAllAsync(string endpoint);
    Task<TOutput?> GetByIdAsync(string endpoint, int id);
    Task<bool> CreateAsync(string endpoint, TInput dto);
    Task<bool> UpdateAsync(string endpoint, int id, TInput dto);
    Task<bool> DeleteAsync(string endpoint, int id);
}