using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Application.Dtos

{
    public class TaskDto // guardar y recuperar los datos de la Tarea => Intermediario entre capa de negocio / lógica y frontend.
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public Priority TaskPriority { get; set; }
        public Status TaskStatus { get; set; } = Status.Pending;
        public DateTime ExpirationDate { get; set; }
        public TaskDto() { }
        public TaskDto(string title, Priority priorityTask, DateTime expirationDate)
        {
            Title = title;
            TaskPriority = priorityTask;
            ExpirationDate = expirationDate;
        }
    }
}
