using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public abstract class Task
    {
        public Guid Id { get; init; }
        public string Title { get; private set; }
        public string? Description { get; set; }
        public Priority TaskPriority { get; private set; }
        public Status TaskStatus { get; private set; } = Status.Pending;
        public DateTime CreationDate { get; private set; } = DateTime.Today;
        public DateTime? CompletionDate { get; private set; }
        public string? CancellationReason { get; private set; }
        public Task(string title, Priority taskPriority, DateTime completionDate, string? description = null)
        {
            Id = Guid.NewGuid();

            Title = string.IsNullOrWhiteSpace(title)
                ? throw new ArgumentException("El título no puede estar vacío") : title;

            CompletionDate = HasExpired()
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : completionDate;

            TaskPriority = taskPriority;
             
            Description = description;
        }
        public void Start() {
            if (TaskStatus != Status.Pending)
                throw new InvalidOperationException("Solo se puede iniciar una tarea pendiente.");
            TaskStatus = Status.InProgress;
        }
        public void Complete()
        {
            if (TaskStatus != Status.InProgress)
                throw new InvalidOperationException("Solo se puede completar una tarea en proceso.");
            TaskStatus = Status.Completed;
        }

        public void Cancel(string reason)
        {
            if (TaskStatus != Status.InProgress)
                throw new InvalidOperationException("Solo se puede cancelar una tarea en proceso.");
            TaskStatus = Status.Cancelled;
            CancellationReason = reason;
        }
        public bool HasExpired() => CompletionDate < DateTime.Today;

        public int CalcRemainingTime()
        {
            if (!CompletionDate.HasValue) return 0;
            return (CompletionDate.Value.Date - DateTime.Today).Days;
        }
        public void ChangePriority(Priority newPriority)
        {
            if (TaskStatus == Status.Cancelled)
                throw new InvalidOperationException("No se puede cambiar la prioridad de una tarea cancelada.");
            if (TaskStatus == Status.Completed)
                throw new InvalidOperationException("No se puede cambiar la prioridad de una tarea completada.");
            TaskPriority = newPriority;
        }

        public abstract override string ToString();
    }
}
