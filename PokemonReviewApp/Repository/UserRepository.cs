using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Security.Cryptography;

namespace PokemonReviewApp.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            //kullanıcıyı veritabanında bul
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

            if (user == null)
                return null; //kullanıcı yoksa null dön

            //şifreyi doğrula
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt)) //veritabanındaki salt'ı anahtar olarak veriyoruz
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<bool> ResetPassword(string username, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

            if (user == null)
                return false;

            //yeni şifre için yeni bir hash ve salt üretiyor
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserOutputDto>> GetUsersWithRolesAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .Select(u => new UserOutputDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Name = u.Name,
                    Surname = u.Surname,
                    Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList(),
                    Permissions = u.UserRoles
                        .SelectMany(ur => ur.Role.RolePermissions)
                        .Select(rp => rp.Permission.Name)
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<List<string>> GetUserPermissions(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Name)
                .Distinct() //birden fazla rolde olan aynı yetkileri tekrarlamıyor
                .ToListAsync();
        }

    }
}