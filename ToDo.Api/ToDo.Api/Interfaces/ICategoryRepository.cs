using ToDo.Api.DataAccess.Models;

namespace ToDo.Api.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category?> GetByIdAsync(int id);
        public Task<IEnumerable<Category>> GetByUserIdAsync(int userId);
        public Task<Category> CreateAsync(Category category);
        public Task<Category?> UpdateAsync(Category category, int id);
        public Task<Category> DeleteAsync(int id);
    }
}
