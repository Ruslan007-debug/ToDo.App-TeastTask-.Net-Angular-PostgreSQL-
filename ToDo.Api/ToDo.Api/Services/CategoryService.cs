using ToDo.Api.DataAccess.DTOs;
using ToDo.Api.Interfaces;
using ToDo.Api.Interfaces.ServicesInterfaces;
using ToDo.Api.Mappers;
using static ToDo.Api.DataAccess.DTOs.CategoryDTOs;

namespace ToDo.Api.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto, int userId)
        {
            var category = dto.ToCategoryFromCreateDTO(userId);

            var createdCategory = await _categoryRepository.CreateAsync(category);
            return createdCategory.ToCategoryDTO();
        }

        public async Task<CategoryDTO?> DeleteAsync(int id, int userId)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return null;
            }
            if (existingCategory.UserId != userId)
            {
                return null;
            }
            var deletedCategory = await _categoryRepository.DeleteAsync(id);
            if (deletedCategory == null)
            {
                return null;
            }
            return deletedCategory.ToCategoryDTO();
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id, int userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            if (category.UserId != userId)
            {
                return null;
            }
            return category.ToCategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetByUserIdAsync(int userId)
        {
            var categories = await _categoryRepository.GetByUserIdAsync(userId);
            return categories.Select(c=>c.ToCategoryDTO());
        }

        public async Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto, int userId)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return null;
            }
            if (existingCategory.UserId != userId)
            {
                return null;
            }
            var updatedCategoryEntity = dto.ToCategoryFromUpdateDTO();
            var updatedCategory = await _categoryRepository.UpdateAsync(updatedCategoryEntity, id);
            if (updatedCategory==null)
            {
                return null;
            }
            return updatedCategory.ToCategoryDTO();
        }
    }
}
