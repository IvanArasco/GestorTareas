using GestorDeTareas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Application.Dtos

{
    public class TaskRequestDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Title { get; set; }
        [Required]
        public Priority TaskPriority { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public DevelopmentArea DevelopmentArea { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
