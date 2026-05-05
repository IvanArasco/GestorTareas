using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeTareas.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET /api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        // GET /api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST /api/users
        [HttpPost]
        public IActionResult AddUser([FromBody] UserRequestDto userDto)
        {
            var user = _userService.Create(userDto.Name, userDto.Email, userDto.Birthday, userDto.IsAdmin);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // DELETE /api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}