using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IRoleInterface
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role?> GetRole(int id);
        Task<bool> RoleExists(string name);
        Task<bool> RoleExistsById(int id);
        Task<bool> CreateRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(Role role);
    }
}