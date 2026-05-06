using GestorDeTareas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Application.Dtos

{
    public class TaskRequestDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public Priority TaskPriority { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public DevelopmentArea DevelopmentArea { get; set; }
        [Required]
        public string TipoTarea { get; set; } = string.Empty;
        [Required]
        public int UserId { get; set; }

        // Bug fields
        public string? ExpectedBehaviour { get; private set; }
        public string? ActualBehaviour { get; private set; }

        // Improvement fields
        public string? AffectedFeature { get; private set; }
        public string? ExpectedBenefict { get; private set; }

        // New feature fields
        public DevelopmentArea? Area { get; private set; }

        // RecurringTask fields
        public Frequency? Frequency { get; private set; }
        public DateTime? LastExecution { get; private set; }
        public DateTime? NextExecution { get; private set; }
    }
}
