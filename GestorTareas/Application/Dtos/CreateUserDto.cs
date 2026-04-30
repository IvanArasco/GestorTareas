using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Application.Dtos
{
    public class CreateUserDto
    {
        // Nombre y Email obligatorios. Nombre máximo 100 caracteres.

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
        public string Email { get; set; } = string.Empty;
    }
}
