using ToDo.Api.DataAccess.Models;
using static ToDo.Api.DataAccess.DTOs.CategoryDTOs;

namespace ToDo.Api.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UserId = category.UserId
            };
        }

        public static Category ToCategoryFromCreateDTO(this CreateCategoryDTO dto, int userId)
        {
            return new Category
            {
                Name = dto.Name,
                UserId = userId
            };
        }

        public static Category ToCategoryFromUpdateDTO(this UpdateCategoryDTO dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }
    }
}
