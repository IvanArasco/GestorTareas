using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Application.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress(ErrorMessage = "Formato de email no válido")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Birthdate { get; set; } = string.Empty;

        [Required, MinLength(3, ErrorMessage = "Mínimo 3 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
}
