using Microsoft.EntityFrameworkCore;
using ToDo.Api.DataAccess.Data;
using ToDo.Api.DataAccess.Models;
using ToDo.Api.Interfaces;

namespace ToDo.Api.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoDbContext _context;
        public TaskRepository(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return await _context.Tasks
                        .Include(t => t.Category)
                        .FirstAsync(t => t.Id == task.Id);
        }

        public async Task<TaskItem?> DeleteAsync(int id)
        {
            var deleting = await _context.Tasks.FindAsync(id);
            if (deleting == null)
            {
                return null!;
            }
            _context.Tasks.Remove(deleting);
            await _context.SaveChangesAsync();
            return deleting;
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetFilteredAsync(
            int userId, string? searchTerm, int? categoryId, int page, int pageSize)     //метод для отримання задач конкретного користувача, пошуку по назві задачі та фільтрації по категорії, з пагінацією
        {
            var query = _context.Tasks.Include(t => t.Category).Where(t => t.UserId == userId);

            if (!string.IsNullOrEmpty(searchTerm))      //фільтрація по назві задачі
            {
                query = query.Where(t => EF.Functions.ILike(t.Title, $"%{searchTerm}%"));
            }

            if(categoryId.HasValue)         //фільтрація по категорії
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }

            var totalCount = await query.CountAsync();
            var items = await query         //отримання задач з пагінацією, приклад: якщо сторінка 1, а розмір сторінки 10, то пропускаємо 0 задач і беремо 10, і тд
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, totalCount);
        }

        public async Task<TaskItem?> UpdateAsync(TaskItem task, int id)
        {
            var updating = await _context.Tasks.FindAsync(id);
            if (updating == null)
            {
                return null!;
            }
            updating.Title = task.Title;
            updating.Description = task.Description;
            updating.DueDate = task.DueDate;
            updating.IsCompleted = task.IsCompleted;
            updating.CategoryId = task.CategoryId;
            await _context.SaveChangesAsync();
            return await _context.Tasks
                        .Include(t => t.Category)
                        .FirstAsync(t => t.Id == id);
        }
    }
}
