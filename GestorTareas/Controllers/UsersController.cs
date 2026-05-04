using GestorDeTareas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly TaskContext _context;

        public UsersController(TaskContext context)
        {
            _context = context;
        }

        // GET /api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users
                .Include(u => u.Tasks)
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    Tasks = u.Tasks
                })
                .ToList();

            return Ok(users);
        }

        // GET /api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                Tasks = user.Tasks.ToList()
            });
        }

        // POST /api/users
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // DELETE /api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}