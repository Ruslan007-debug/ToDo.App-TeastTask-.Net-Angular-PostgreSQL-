using Microsoft.EntityFrameworkCore;
using ToDo.Api.DataAccess.Data;
using ToDo.Api.DataAccess.Models;
using ToDo.Api.Interfaces;

namespace ToDo.Api.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoDbContext _context;
        public UserRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<User?> UpdateAsync(User user, int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.RefreshToken = user.RefreshToken;
            existingUser.RefreshTokenExpiryTime = user.RefreshTokenExpiryTime;

            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}
