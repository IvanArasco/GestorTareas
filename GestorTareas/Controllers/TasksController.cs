using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class TasksController : ControllerBase
{
    private readonly TaskManagerContext _context;
    public TasksController(TaskManagerContext context)
    {
        _context = context;
    }
    /*
    // GET /api/tareas
    [HttpGet]
    public IActionResult GetAll()
    {

        var tareas = _context.Tasks
        .Include(t => t.User)
        .Select(t => new
        {
            t.Id,
            t.Titulo,
            t.EstaCompletada,
            t.FechaLimite,
            Usuario = t.Usuario.Nombre
        })
        .ToList();

        return Ok(tareas);
    }

    // GET /api/tareas/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var tarea = _context.Tasks
        .Include(t => t.User)
        .FirstOrDefault(t => t.Id == id);

        if (tarea == null)
            return NotFound();

        return Ok(new
        {
            tarea.Id,
            tarea.Titulo,
            tarea.EstaCompletada,
            tarea.FechaLimite,
            Usuario = tarea.Usuario.Nombre
        });
    }

    // PUT /api/tareas/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CrearTareaDto dto)
    {
        var tarea = _context.Tareas.Find(id);
        if (tarea == null) return NotFound();

        tarea.Titulo = dto.Titulo;
        tarea.FechaLimite = dto.FechaLimite;
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