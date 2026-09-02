using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IUserInterface
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> ResetPassword(string username, string newPassword);
        Task<bool> UserExists(string username);
    }
}
