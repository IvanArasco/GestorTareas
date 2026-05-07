using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;
    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    // GET /api/tasks
    [HttpGet]
    public IActionResult GetAll()
    {
        var tasks = _taskService.GetAll();

        return Ok(tasks);
    }

    // GET /api/tasks/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _taskService.GetById(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    //POST /api/tasks
    [HttpPost]
    public IActionResult AddTask([FromBody] TaskRequestDto taskDto)
    {

        // Obtener el Id del usuario autenticado desde el token
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr == null) return Unauthorized();

        int userId = int.Parse(userIdStr);

        var response = _taskService.Create(taskDto, userId);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    // DELETE /api/tasks/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        try
        {
            _taskService.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}