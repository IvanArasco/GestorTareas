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

            CompletionDate = completionDate < DateTime.Today
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : completionDate;

            TaskPriority = taskPriority;
             
            Description = description;
        }
        public void Start() {
            if (TaskStatus != Status.Pending || HasExpired())
                throw new InvalidOperationException("Solo se puede iniciar una tarea pendiente o que no haya expirado.");
            TaskStatus = Status.InProgress;
        }
        public void Complete()
        {
            if (TaskStatus != Status.InProgress || HasExpired())
                throw new InvalidOperationException("Solo se puede completar una tarea en proceso o que no haya expirado.");
            TaskStatus = Status.Completed;
        }

        public void Cancel(string reason)
        {
            if (TaskStatus != Status.InProgress && TaskStatus != Status.Pending || HasExpired())
                throw new InvalidOperationException("Solo se puede cancelar una tarea pendiente, en proceso o que no haya expirado.");
            TaskStatus = Status.Cancelled;
            CancellationReason = reason;
        }
        public bool HasExpired() => CompletionDate.HasValue 
            && CompletionDate < DateTime.Today 
            && TaskStatus != Status.Completed 
            && TaskStatus != Status.Cancelled;

        public int CalcRemainingTime()
        {
            if (!CompletionDate.HasValue || HasExpired()) return 0;
            return (CompletionDate.Value.Date - DateTime.Today).Days;
        }
        public void ChangePriority(Priority newPriority)
        {
            if (TaskStatus == Status.Cancelled || TaskStatus == Status.Completed || HasExpired())
                throw new InvalidOperationException("No se puede cambiar la prioridad de una tarea completada, cancelada o expirada.");
            TaskPriority = newPriority;
        }

        public abstract override string ToString();
    }
}
