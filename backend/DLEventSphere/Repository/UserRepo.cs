using DLEventSphere.Abstract;
using DLEventSphere.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly EventContext _context;

        public UserRepo(EventContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.Name)
                .ToListAsync();
        }

        public async Task<User?> AddUserAsync(User user)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Email.ToLower() == user.Email.ToLower());

            if (exists)
                return null;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByIdAsync(long userId)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.UserId);
            if (existing == null) return null;

            existing.Name = user.Name;
            existing.Email = user.Email;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUserAsync(long userId)
        {
            var existing = await _context.Users
                .Include(u => u.CreatedEvents)
                .Include(u => u.Registrations)
                .Include(u => u.Feedbacks)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (existing == null)
                return false;

            if ((existing.CreatedEvents?.Any() ?? false) ||
                (existing.Registrations?.Any() ?? false) ||
                (existing.Feedbacks?.Any() ?? false))
                throw new InvalidOperationException(
                    "Cannot delete user with related data.");

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserByEmail(string mailId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == mailId.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Role role)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.role == role)
                .ToListAsync();
        }
    }
}