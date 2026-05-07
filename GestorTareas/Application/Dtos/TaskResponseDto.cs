using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Application.Dtos
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Status TaskStatus { get; set; } = Status.Pending;
        public Priority TaskPriority { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string TaskType { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

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
