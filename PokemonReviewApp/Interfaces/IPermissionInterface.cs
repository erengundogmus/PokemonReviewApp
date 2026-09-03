using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPermissionInterface
    {
        Task<bool> CreatePermission(Permission permission);
        Task<bool> PermissionExists(string name);
        Task<bool> PermissionExistsById(int id); // Yeni eklendi
        Task<bool> AssignPermissionToRole(int roleId, int permissionId);
        Task<bool> RemovePermissionFromRole(int roleId, int permissionId); // Yeni eklendi
        Task<bool> RoleHasPermission(int roleId, int permissionId);
    }
}