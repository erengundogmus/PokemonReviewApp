using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PermissionRepository : IPermissionInterface
    {
        private readonly DataContext _context;

        public PermissionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePermission(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> PermissionExists(string name)
        {
            return await _context.Permissions.AnyAsync(p => p.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> AssignPermissionToRole(int roleId, int permissionId)
        {
            var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permissionId };
            await _context.RolePermissions.AddAsync(rolePermission);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RoleHasPermission(int roleId, int permissionId)
        {
            return await _context.RolePermissions.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }

        public async Task<bool> PermissionExistsById(int id)
        {
            return await _context.Permissions.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> RemovePermissionFromRole(int roleId, int permissionId)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermission == null) return false;

            _context.RolePermissions.Remove(rolePermission);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}