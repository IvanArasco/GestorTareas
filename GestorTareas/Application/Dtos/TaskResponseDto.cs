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
        public string User { get; set; } = string.Empty;

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
