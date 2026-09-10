namespace ToDo.Api.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    }
}
