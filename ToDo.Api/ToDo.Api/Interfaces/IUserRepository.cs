using ToDo.Api.DataAccess.Models;

namespace ToDo.Api.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetByIdAsync(int id);
        public Task<User?> GetByEmailAsync(string email);
        public Task<User> CreateAsync(User user);
        public Task<User?> GetByRefreshTokenAsync(string refreshToken);
        public Task<User?> UpdateAsync(User user, int id);

    }
}
