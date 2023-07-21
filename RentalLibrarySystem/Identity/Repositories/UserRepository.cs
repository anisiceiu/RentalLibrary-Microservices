using Identity.Data;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly IdentityContext _context;
        public UserRepository(IdentityContext context)
        {
            _context = context;
        }
        public async Task<Role> AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<UserRole>> AssignRolesAsync(List<UserRole> userRoles)
        {
            await _context.UserRoles.AddRangeAsync(userRoles);
            await _context.SaveChangesAsync();
            return userRoles;
        }

        public async Task<User> AddMemberUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userRole = new UserRole { UserId = user.UserId, RoleId = 2 };//2=User
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User?> AuthenticateUser(string userName, string password)
        {
            string passwordHash = this.ComputeSha256Hash(password);
            var loginUser = await _context.Users.Where(u => u.PasswordHash == passwordHash && u.Username == userName).FirstOrDefaultAsync();

            return loginUser;
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<List<Role>> GetRolesByUserIdAsync(int userId)
        {
            var roleIds = await _context.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToListAsync();
            return await _context.Roles.Where(r => roleIds.Contains(r.RoleId)).ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }
        public string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
