using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Application.Dtos
{
    public class UserRequestDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateOnly Birthdate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
