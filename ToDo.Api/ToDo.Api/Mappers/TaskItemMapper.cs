using ToDo.Api.DataAccess.Models;
using static ToDo.Api.DataAccess.DTOs.TaskItemDTOs;

namespace ToDo.Api.Mappers
{
    public static class TaskItemMapper
    {
        public static TaskItemDTO ToTaskItemDTO(TaskItem taskItem)
        {
            return new TaskItemDTO
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted,
                DueDate = taskItem.DueDate,
                CreatedAt = taskItem.CreatedAt,
                UserId = taskItem.UserId,
                CategoryId = taskItem.CategoryId
            };
        }

        public static TaskItem ToTaskItemFromCreateDTO(CreateTaskItemDTO dto, int userId)
        {
            return new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted,
                DueDate = dto.DueDate,
                UserId = userId,
                CategoryId = dto.CategoryId
            };
        }

        public static TaskItem ToTaskItemFromUpdateDTO(UpdateTaskItemDTO dto)
        {
            return new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted,
                DueDate = dto.DueDate,
                CategoryId = dto.CategoryId
            };
        }
    }
}
