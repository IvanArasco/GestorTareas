using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = GestorDeTareas.Domain.Entities.Task;

public class TasksController : ControllerBase
{
    private readonly TaskManagerContext _context;
    public TasksController(TaskManagerContext context)
    {
        _context = context;
    }
    // GET /api/tareas
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

    // GET /api/tareas/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var tasks = _context.Tasks
        .Include(t => t.User)
        .FirstOrDefault(t => t.Id == id);

        if (tasks == null)
            return NotFound();

        return Ok(new
        {
            tasks.Id,
            tasks.Title,
            tasks.ExpirationDate,
            Usuario = tasks.User.Name
        });
    }

    // PUT /api/tareas/1
    /*
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Task task)
    {
        var tasks = _context.Tasks.Find(id);
        if (task == null) return NotFound();

        task.Title = tasks.Title;
        task.ExpirationDate = tasks.ExpirationDate;
        _context.SaveChanges();

        return NoContent();
    }
    */

    // DELETE /api/tareas/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var tarea = _context.Tasks.Find(id);
        if (tarea == null) return NotFound();

        _context.Tasks.Remove(tarea);
        _context.SaveChanges();

        return NoContent();
    }
}