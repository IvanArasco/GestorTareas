using GestorDeTareas.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tasks")]
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
        var tasks = _taskService.GetAll()
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.ExpirationDate,
                User = t.User.Name
            })
            .ToList();

        return Ok(tasks);
    }

    // GET /api/tasks/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _taskService.GetById(id);

        if (task == null)
            return NotFound();

        return Ok(new
        {
            task.Id,
            task.Title,
            task.ExpirationDate,
            User = task.User.Name
        });
    }

    // DELETE /api/tasks/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _taskService.Delete(id);
 
        return NoContent();
    }
}