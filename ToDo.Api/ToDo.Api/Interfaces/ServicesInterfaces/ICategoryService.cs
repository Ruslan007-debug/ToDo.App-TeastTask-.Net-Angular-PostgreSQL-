using static ToDo.Api.DataAccess.DTOs.CategoryDTOs;

namespace ToDo.Api.Interfaces.ServicesInterfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetByUserIdAsync(int userId);

        Task<CategoryDTO?> GetByIdAsync(int id, int userId);

        Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto, int userId);

        Task<CategoryDTO?> UpdateAsync(int id, UpdateCategoryDTO dto, int userId);

        Task<CategoryDTO?> DeleteAsync(int id,int userId);
    }
}
