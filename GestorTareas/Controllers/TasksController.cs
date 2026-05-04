using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = GestorDeTareas.Domain.Entities.Task;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskContext _context;
    public TasksController(TaskContext context)
    {
        _context = context;
    }

    // GET /api/tasks
    [HttpGet]
    public IActionResult GetAll()
    {
        var tasks = _context.Tasks
            .Include(t => t.User)
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
        var task = _context.Tasks
            .Include(t => t.User)
            .FirstOrDefault(t => t.Id == id);

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

    // PUT /api/tasks/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Task task)
    {
        var existingTask = _context.Tasks.Find(id);
        if (existingTask == null)
            return NotFound();

        existingTask.Title = task.Title;
        existingTask.ExpirationDate = task.ExpirationDate;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE /api/tasks/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null)
            return NotFound();

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return NoContent();
    }
}