using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        => _authService = authService;

        // POST /api/auth/registro
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = _authService.Register(dto);
                if (result == null)
                    return Conflict("El email ya está registrado");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST /api/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var result = _authService.Login(dto);
            if (result == null)
                return Unauthorized("Credenciales incorrectas");
            return Ok(result);
        }
    }
}
