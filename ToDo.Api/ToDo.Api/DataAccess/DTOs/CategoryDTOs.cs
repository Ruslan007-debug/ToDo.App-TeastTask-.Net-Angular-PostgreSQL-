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
            public string Name { get; set; } = string.Empty;
        }

        public class UpdateCategoryDTO
        {
            public string Name { get; set; } = string.Empty;
        }
    }
}
