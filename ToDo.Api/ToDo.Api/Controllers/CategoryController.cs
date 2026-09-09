using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Interfaces.ServicesInterfaces;
using static ToDo.Api.DataAccess.DTOs.CategoryDTOs;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetByUserId([FromQuery] int userId)
        {
            var categories = await _categoryService.GetByUserIdAsync(userId);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] int userId)
        {
            var category = await _categoryService.GetByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto, [FromQuery] int userId)
        {
            var category = await _categoryService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = category.Id, userId = userId }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] int userId, [FromBody] UpdateCategoryDTO dto)
        {
            var category = await _categoryService.UpdateAsync(id, dto, userId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var category = await _categoryService.DeleteAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
