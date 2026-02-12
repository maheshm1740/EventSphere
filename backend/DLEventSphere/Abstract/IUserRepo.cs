using DLEventSphere.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Abstract
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> AddUserAsync(User user);

        Task<User?> GetUserByIdAsync(long userId);

        Task<User?> GetUserByEmail(string mailId);

        Task<User?> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(long id);

        Task<IEnumerable<User>> GetUsersByRoleAsync(Role role);
    }
}
