using ToDo.Api.DataAccess.DTOs;
using static ToDo.Api.DataAccess.DTOs.TaskItemDTOs;

namespace ToDo.Api.Interfaces.ServicesInterfaces
{
    public interface ITaskItemService
    {
        public Task<PagedResultDTO<TaskItemDTO>> GetFilteredAsync(
            int userId,
            string? searchTerm,
            int? categoryId,
            int page,
            int pageSize);
        public Task<TaskItemDTO?> GetByIdAsync(int id, int userId);
        public Task<TaskItemDTO?> CreateAsync(CreateTaskItemDTO dto, int userId);
        public Task<TaskItemDTO?> UpdateAsync(int id, UpdateTaskItemDTO dto, int userId);
        public Task<TaskItemDTO?> DeleteAsync(int id, int userId);
    }
}
