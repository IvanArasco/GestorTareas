using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public abstract class Task
    {
        public int Id { get; init; }
        public string Title { get; private set; }
        public string? Description { get; set; }
        public Priority Priority { get; private set; }
        public Status TaskStatus { get; private set; } = Status.Pending;
        public DateTime CreationDate { get; private set; } = DateTime.Today;
        public DateTime ExpirationDate { get; private set; }
        public string? CancellationReason { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        public Task(string title, Priority taskPriority, DateTime expirationDate, int userId, string? description = null)
        {

            Title = string.IsNullOrWhiteSpace(title)
                ? throw new ArgumentException("El título no puede estar vacío") : title;

            ExpirationDate = expirationDate < DateTime.Today
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : expirationDate;

            UserId = userId <= 0 ? throw new ArgumentException("El ID de usuario no puede ser 0 o negativo") : userId;

            Priority = taskPriority;
             
            Description = description;
        }
        public void changeTitle(string newTitle)
        {
            Title = newTitle;
        }
        public void changeExpirationTime(DateTime newDate)
        {
            ExpirationDate = newDate;
        }
        public void Start()
        {
            if (TaskStatus != Status.Pending)
                throw new InvalidOperationException("La tarea debe estar pendiente para iniciarse.");
            if (HasExpired())
                throw new InvalidOperationException("No se puede iniciar una tarea expirada.");
            if (TaskStatus == Status.InProgress)
                throw new InvalidOperationException("No se puede iniciar una tarea iniciada.");
            TaskStatus = Status.InProgress;
        }
        public void Cancel(string reason)
        {
            if (TaskStatus == Status.Completed || TaskStatus == Status.Cancelled)
                throw new InvalidOperationException("No se puede cancelar una tarea ya completada o cancelada.");
            if (HasExpired())
                throw new InvalidOperationException("No se puede cancelar una tarea expirada.");
            if (TaskStatus == Status.Cancelled)
                throw new InvalidOperationException("No se puede iniciar una tarea cancelada.");
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("El motivo de cancelación no puede estar vacío.", nameof(reason));

            TaskStatus = Status.Cancelled;
            CancellationReason = reason;
        }
        public void Complete()
        {
            if (TaskStatus != Status.InProgress)
                throw new InvalidOperationException("La tarea debe estar en progreso para completarse.");
            if (HasExpired())
                throw new InvalidOperationException("No se puede completar una tarea expirada.");
            if (TaskStatus == Status.Completed)
                throw new InvalidOperationException("No se puede iniciar una tarea completada.");

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
