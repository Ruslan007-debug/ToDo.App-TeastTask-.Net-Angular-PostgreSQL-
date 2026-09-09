using System.ComponentModel.DataAnnotations;

namespace ToDo.Api.DataAccess.DTOs
{
    public class CategoryDTOs
    {
        public class CategoryDTO
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int UserId { get; set; }
        }

        public class CreateCategoryDTO
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; } = string.Empty;
        }

        public class UpdateCategoryDTO
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; } = string.Empty;
        }
    }
}
