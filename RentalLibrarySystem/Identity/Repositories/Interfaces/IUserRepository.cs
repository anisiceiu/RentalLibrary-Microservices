using Identity.Data;
using Identity.Entities;
using System.Text;

namespace Identity.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Role> AddRoleAsync(Role role);
        Task<User> AddUserAsync(User user);
        Task<List<UserRole>> AssignRolesAsync(List<UserRole> userRoles);
        Task<User?> AuthenticateUser(string userName, string password);
        Task<List<Role>> GetRolesAsync();
        string ComputeSha256Hash(string password);
        Task<User> AddMemberUserAsync(User user);
        Task<List<Role>> GetRolesByUserIdAsync(int userId);
        Task<User?> GetUserById(int id);
    }
}
