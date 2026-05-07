using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestorDeTareas.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repositorio, IConfiguration config)
        {
            _repository = repositorio;
            _config = config;
        }

        public async Task<TokenResponseDto?> Registrar(RegisterDto dto)
        {
            // Verificar que el email no está en uso
            if (_repository.GetByEmail(dto.Email) != null)
                return null; // email ya registrado

            // Crear el usuario con la contraseña hasheada
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Birthdate = dto.Birthdate, // no hay en el DTO pero el servicio lo usa
                IsAdmin = false,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            _repository.AddUser(user);

            return GenerarToken(user);
        }

        public TokenResponseDto? Login(LoginDto dto)
        {
            var usuario = _repository.GetByEmail(dto.Email);
            if (usuario == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                return null;

            return GenerarToken(usuario);
        }

        private TokenResponseDto GenerarToken(User user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(
            int.Parse(_config["Jwt:ExpirationMinutes"]!));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                };

            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

            return new TokenResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expira = expiration
            };
        }
    }
}
