using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public abstract class Task
    {
        public Guid Id { get; init; }
        public string Title { get; private set; }
        public string? Description { get; set; }
        public Priority Priority { get; private set; }
        public Status TaskStatus { get; private set; } = Status.Pending;
        public DateTime CreationDate { get; private set; } = DateTime.Today;
        public DateTime ExpirationDate { get; private set; }
        public string? CancellationReason { get; private set; }
        public Task(string title, Priority taskPriority, DateTime completionDate, string? description = null)
        {
            Id = Guid.NewGuid();

            Title = string.IsNullOrWhiteSpace(title)
                ? throw new ArgumentException("El título no puede estar vacío") : title;

            ExpirationDate = completionDate < DateTime.Today
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : completionDate;

            Priority = taskPriority;
             
            Description = description;
        }
        public void Start()
        {
            if (TaskStatus != Status.Pending)
                throw new InvalidOperationException("La tarea debe estar pendiente para iniciarse.");
            if (HasExpired())
                throw new InvalidOperationException("No se puede iniciar una tarea expirada.");

            TaskStatus = Status.InProgress;
        }
        public void Cancel(string reason)
        {
            if ((TaskStatus != Status.InProgress && TaskStatus != Status.Pending) || HasExpired())
                throw new InvalidOperationException("Solo se puede cancelar una tarea pendiente, en proceso o que no haya expirado.");
            TaskStatus = Status.Cancelled;
            CancellationReason = reason;
        }
        public void Complete()
        {
            if (TaskStatus != Status.InProgress)
                throw new InvalidOperationException("La tarea debe estar en progreso para completarse.");
            if (HasExpired())
                throw new InvalidOperationException("No se puede iniciar una tarea expirada.");

            TaskStatus = Status.Completed;
        }

        public bool HasExpired() => ExpirationDate < DateTime.Today 
            && TaskStatus != Status.Completed 
            && TaskStatus != Status.Cancelled;

        public int CalcRemainingDays() => HasExpired() ? 0 : (ExpirationDate.Date - DateTime.Today).Days;
        
        public void ChangePriority(Priority newPriority)
        {
            if (TaskStatus == Status.Cancelled || TaskStatus == Status.Completed || HasExpired())
                throw new InvalidOperationException("No se puede cambiar la prioridad de una tarea completada, cancelada o expirada.");
            Priority = newPriority;
        }

        public abstract override string ToString();
    }
}
