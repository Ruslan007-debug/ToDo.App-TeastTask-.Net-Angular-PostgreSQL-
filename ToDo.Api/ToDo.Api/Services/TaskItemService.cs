using ToDo.Api.DataAccess.DTOs;
using ToDo.Api.Interfaces;
using ToDo.Api.Interfaces.ServicesInterfaces;
using ToDo.Api.Mappers;
using static ToDo.Api.DataAccess.DTOs.TaskItemDTOs;

namespace ToDo.Api.Services
{
    public class TaskItemService: ITaskItemService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TaskItemService(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<TaskItemDTO?> CreateAsync(CreateTaskItemDTO dto, int userId)
        {
            if (dto.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
                if (category == null || category.UserId != userId)
                {
                    return null;
                }
            }
            var taskItem = dto.ToTaskItemFromCreateDTO(userId);
            var createdTask = await _taskRepository.CreateAsync(taskItem);
            return createdTask.ToTaskItemDTO();
        }

        public async Task<TaskItemDTO?> DeleteAsync(int id, int userId)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null || existingTask.UserId != userId)
            {
                return null;
            }
            var deletedTask = await _taskRepository.DeleteAsync(id);
            if (deletedTask == null)
            {
                return null;
            }
            return deletedTask.ToTaskItemDTO();
        }

        public async Task<TaskItemDTO?> GetByIdAsync(int id, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null || task.UserId != userId)
            {
                return null;
            }
            return task.ToTaskItemDTO();
        }

        public async Task<PagedResultDTO<TaskItemDTO>> GetFilteredAsync(
            int userId, string? searchTerm,
            int? categoryId, int page, int pageSize)
        {
            var result = await _taskRepository.GetFilteredAsync(userId, searchTerm, categoryId, page, pageSize);
            var taskDtos = result.Items.Select(t => t.ToTaskItemDTO());
            return new PagedResultDTO<TaskItemDTO>
            {
                Items = taskDtos,
                TotalCount = result.TotalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize)
            };
        }

        public async Task<TaskItemDTO?> UpdateAsync(int id, UpdateTaskItemDTO dto, int userId)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null || existingTask.UserId != userId)
            {
                return null;
            }
            if (dto.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
                if (category == null || category.UserId != userId)
                {
                    return null;
                }
            }
            var taskToUpdate = dto.ToTaskItemFromUpdateDTO();
            var updatedTask = await _taskRepository.UpdateAsync(taskToUpdate, id);
            if (updatedTask == null)
            {
                return null;
            }
            return updatedTask.ToTaskItemDTO();
        }
    }
}
