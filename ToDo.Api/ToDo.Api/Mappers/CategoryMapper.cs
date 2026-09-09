using ToDo.Api.DataAccess.Models;
using static ToDo.Api.DataAccess.DTOs.CategoryDTOs;

namespace ToDo.Api.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToCategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UserId = category.UserId
            };
        }

        public static Category ToCategoryFromCreateDTO(CreateCategoryDTO dto, int userId)
        {
            return new Category
            {
                Name = dto.Name,
                UserId = userId
            };
        }

        public static Category ToCategoryFromUpdateDTO(UpdateCategoryDTO dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }
    }
}
