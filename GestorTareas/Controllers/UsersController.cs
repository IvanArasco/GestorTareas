using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly TaskManagerContext _context;
        public UsersController(TaskManagerContext context)
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
                Task = u.Tasks,
            })
            .ToList();

            return Ok(users);
        }

    }
}
