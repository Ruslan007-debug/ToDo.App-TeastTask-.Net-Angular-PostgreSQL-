using ToDo.Api.DataAccess.Models;

namespace ToDo.Api.Interfaces
{
    public interface ITaskRepository
    {
        public Task<TaskItem?> GetByIdsAsync(int id);
        public Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetFilteredAsync(
            int userId,
            string? searchTerm,
            int? categoryId,
            int page,
            int pageSize);
        public Task<TaskItem> CreateAsync(TaskItem task);
        public Task<TaskItem?> UpdateAsync(TaskItem task, int id);
        public Task<TaskItem> DeleteAsync(int id);
    }
}
