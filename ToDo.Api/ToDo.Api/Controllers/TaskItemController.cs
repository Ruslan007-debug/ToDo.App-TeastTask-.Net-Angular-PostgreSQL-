using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Interfaces.ServicesInterfaces;
using static ToDo.Api.DataAccess.DTOs.TaskItemDTOs;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/taskitems")]
    public class TaskItemController: ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] int userId,
            [FromQuery] string? searchTerm,
            [FromQuery] int? categoryId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var taskItems = await _taskItemService.GetFilteredAsync(userId, searchTerm, categoryId, page, pageSize);
            return Ok(taskItems);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] int userId)
        {
            var taskItem = await _taskItemService.GetByIdAsync(id, userId);
            if (taskItem == null)
            {
                return NotFound();
            }
            return Ok(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskItemDTO dto, [FromQuery] int userId)
        {
            var taskItem = await _taskItemService.CreateAsync(dto, userId);
            if (taskItem == null)
            {
                return BadRequest("Invalid category or user ID.");
            }
            return CreatedAtAction(nameof(GetById), new { id = taskItem.Id, userId = taskItem.UserId }, taskItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskItemDTO dto, [FromQuery] int userId)
        {
            var updatingTask = await _taskItemService.UpdateAsync(id, dto, userId);
            if (updatingTask == null)
            {
                return NotFound();
            }
            return Ok(updatingTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var deletedTask = await _taskItemService.DeleteAsync(id, userId);
            if (deletedTask == null)
            {
                return NotFound();
            }
            return Ok(deletedTask);
        }
    }
}
