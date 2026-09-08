using Microsoft.EntityFrameworkCore;
using ToDo.Api.DataAccess.Data;
using ToDo.Api.DataAccess.Models;
using ToDo.Api.Interfaces;

namespace ToDo.Api.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ToDoDbContext _context;
        public CategoryRepository(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var deleting = await _context.Categories.FindAsync(id);
            if (deleting == null)
            {
                return null!;
            }
            _context.Categories.Remove(deleting);
            await _context.SaveChangesAsync();
            return deleting;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetByUserIdAsync(int userId)
        {
            return await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category?> UpdateAsync(Category category, int id)
        {
            var updating = await _context.Categories.FindAsync(id);
            if (updating == null)
            {
                return null!;
            }
            updating.Name = category.Name;
            await _context.SaveChangesAsync();
            return updating;
        }
    }
}
