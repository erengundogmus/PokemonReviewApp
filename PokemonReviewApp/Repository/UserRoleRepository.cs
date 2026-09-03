using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class UserRoleRepository : IUserRoleInterface
    {
        private readonly DataContext _context;

        public UserRoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignRole(int userId, int roleId)
        {
            var userRole = new UserRole { UserId = userId, RoleId = roleId };
            await _context.UserRoles.AddAsync(userRole);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RevokeRole(int userId, int roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null) return false;

            _context.UserRoles.Remove(userRole);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExists(int userId) => await _context.Users.AnyAsync(u => u.Id == userId);
        public async Task<bool> RoleExists(int roleId) => await _context.Roles.AnyAsync(r => r.Id == roleId);
        public async Task<bool> UserHasRole(int userId, int roleId) =>
            await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }
}