using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class RoleRepository : IRoleInterface
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRole(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> RoleExists(string name)
        {
            return await _context.Roles.AnyAsync(r => r.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> RoleExistsById(int id)
        {
            return await _context.Roles.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> CreateRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}