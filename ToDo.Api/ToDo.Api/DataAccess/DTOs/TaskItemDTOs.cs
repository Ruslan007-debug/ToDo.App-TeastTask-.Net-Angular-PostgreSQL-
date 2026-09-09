namespace ToDo.Api.DataAccess.DTOs
{
    public class TaskItemDTOs
    {
        public class TaskItemDTO
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; } = string.Empty;
            public bool IsCompleted { get; set; } = false;
            public DateTime? DueDate { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public int UserId { get; set; }
            public int? CategoryId { get; set; }
        }
        public class CreateTaskItemDTO
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; } = string.Empty;
            public bool IsCompleted { get; set; } = false;
            public DateTime? DueDate { get; set; }
            public int? CategoryId { get; set; }
        }
        public class UpdateTaskItemDTO
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; } = string.Empty;
            public bool IsCompleted { get; set; } = false;
            public DateTime? DueDate { get; set; }
            public int? CategoryId { get; set; }
        }
    }
}
