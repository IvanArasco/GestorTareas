using GestorDeTareas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Application.Dtos

{
    public class TaskRequestDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public Priority TaskPriority { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string TaskType { get; set; } = string.Empty;
  
        // Bug fields
        public string? ExpectedBehaviour { get; set; }
        public string? ActualBehaviour { get; set; }

        // Improvement fields
        public string? AffectedFeature { get; set; }
        public string? ExpectedBenefict { get; set; }

        // New feature fields
        public DevelopmentArea? Area { get; set; }

        // RecurringTask fields
        public Frequency? Frequency { get; set; }
        public DateTime? LastExecution { get; set; }
        public DateTime? NextExecution { get; set; }
    }
}
