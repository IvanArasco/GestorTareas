using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Application.Dtos
{
    public class TaskResponseDto
    {
        public int Id { get; init; }
        public string Title { get; set; } = string.Empty;
        public Status TaskStatus { get; set; } = Status.Pending;
        public Priority TaskPriority { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string User { get; set; } = string.Empty;
    }
}
