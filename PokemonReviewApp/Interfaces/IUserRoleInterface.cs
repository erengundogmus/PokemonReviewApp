namespace PokemonReviewApp.Interfaces
{
    public interface IUserRoleInterface
    {
        Task<bool> AssignRole(int userId, int roleId);
        Task<bool> RevokeRole(int userId, int roleId);
        Task<bool> UserExists(int userId);
        Task<bool> RoleExists(int roleId);
        Task<bool> UserHasRole(int userId, int roleId);
    }
}