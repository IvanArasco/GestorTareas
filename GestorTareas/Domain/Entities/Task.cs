/*

Aseguraos de que vuestra colección de tareas es un List<Tarea> con capacidad inicial estimada. -> Capacity : 30 tareas

2. Implementad algún mecanismo de acceso rápido por identificador (Dictionary u otra estructura que justifiquéis).
Los métodos que devuelvan colecciones deben exponer el tipo más restrictivo posible (IEnumerable<T>,
IReadOnlyList<T>...).

3. Añadid al menos un método de búsqueda que acepte un criterio externo como parámetro (Func<Tarea, bool> o
equivalente).

Meter en un README.md

*/
using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public abstract class Task
    {
        public Guid Id { get; init; }
        public string Title { get; private set; }
        public string? Description { get; set; }
        public Priority TaskPriority { get; set; }
        public Status TaskStatus { get; private set; } = Status.Pending;
        public DateTime CreationDate { get; set; } = DateTime.Today;
        public DateTime? CompletionDate { get; set; }
        public string CancellationReason { get; private set; }
        public Task(string title, Priority taskPriority, DateTime completionDate, string description = null)
        {
            Id = Guid.NewGuid();

            Title = string.IsNullOrWhiteSpace(title)
                ? throw new ArgumentException("El título no puede estar vacío") : title;

            CompletionDate = (completionDate.Date < DateTime.Today)
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : completionDate;

            TaskPriority = taskPriority;
             
            Description = description;
        }
        public void Start() => TaskStatus = Status.InProgress;

        public void Complete() => TaskStatus = Status.Completed;

        public void Cancel(string reason)
        {
            TaskStatus = Status.Cancelled;
            CancellationReason = reason;
        }
        public virtual bool HasExpired() => CompletionDate < DateTime.Today;

        public int CalcRemainingTime() => (CompletionDate - DateTime.Today).Days;

        public abstract override string ToString();
    }
}
