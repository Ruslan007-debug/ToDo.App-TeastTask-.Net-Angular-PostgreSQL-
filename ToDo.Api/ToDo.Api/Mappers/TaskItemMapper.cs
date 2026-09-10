using System.Threading.Tasks;
using ToDo.Api.DataAccess.Models;
using static ToDo.Api.DataAccess.DTOs.TaskItemDTOs;

namespace ToDo.Api.Mappers
{
    public static class TaskItemMapper
    {
        public static TaskItemDTO ToTaskItemDTO(this TaskItem taskItem)
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
                CategoryId = taskItem.CategoryId,
                Category = taskItem.Category == null? null: taskItem.Category.ToCategoryDTO()
            };
        }

        public static TaskItem ToTaskItemFromCreateDTO(this CreateTaskItemDTO dto, int userId)
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

        public static TaskItem ToTaskItemFromUpdateDTO(this UpdateTaskItemDTO dto)
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
