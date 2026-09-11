using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Extensions;
using ToDo.Api.Interfaces.ServicesInterfaces;
using static ToDo.Api.DataAccess.DTOs.CategoryDTOs;

namespace ToDo.Api.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.GetUserId();
            var categories = await _categoryService.GetByUserIdAsync(userId);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.GetUserId();
            var category = await _categoryService.GetByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            var userId = User.GetUserId();
            var category = await _categoryService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = category.Id, userId = userId }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDTO dto)
        {
            var userId = User.GetUserId();
            var category = await _categoryService.UpdateAsync(id, dto, userId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();
            var category = await _categoryService.DeleteAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
