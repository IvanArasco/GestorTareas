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

    // GET /api/tasks/by-user/id
    [HttpGet("by-user/{id}")]
    public IActionResult GetTasksByUserId(int id)
    {
        var tasks = _taskService.GetTasksByUserId(id);
        return Ok(tasks);
    }

    //POST /api/tasks
    [HttpPost]
    public IActionResult AddTask([FromBody] TaskRequestDto taskDto)
    {
        // Get auth user from token
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr == null) return Unauthorized();

        int userId = int.Parse(userIdStr);

        try
        {
            var response = _taskService.Create(taskDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT /api/tasks/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TaskRequestDto taskDto)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr == null) return Unauthorized();
        int userId = int.Parse(userIdStr);

        var isAdmin = User.IsInRole("Admin");

        try
        {
            // Delegamos la validación de existencia y permisos al servicio en una sola transacción
            var response = _taskService.Update(id, taskDto, userId, isAdmin);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException) // Nueva excepción para el control de permisos
        {
            return Forbid();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PATCH /api/tasks/{id}/complete
    [HttpPatch("{id}/complete")]
    public IActionResult Complete(int id)
    {
        try
        {
            _taskService.Complete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PATCH /api/tasks/{id}/start
    [HttpPatch("{id}/start")]
    public IActionResult Start(int id)
    {
        try
        {
            _taskService.Start(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/tasks/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr == null) return Unauthorized();
        int userId = int.Parse(userIdStr);
        var isAdmin = User.IsInRole("Admin");

        try
        {
            _taskService.Delete(id, userId, isAdmin);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}