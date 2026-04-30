using GestorDeTareas.Application.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;
using GestorDeTareas.Domain.Enums;

// TO DO - Entity Framework , Delete JSON for real DB Connection.

namespace GestorDeTareas.Domain.Entities
{
    public class TaskManager
    {
        // lista por defecto de tareas que van a componer el JSON inicial y también utilizadas para pruebas
        private List<TaskDto> tareasDto = [
            new TaskDto("Tarea 001", Priority.High, DateTime.Today.AddDays(5)),
            new TaskDto("Tarea 002", Priority.High, DateTime.Today.AddDays(5)),
            new TaskDto("Tarea 003", Priority.High, DateTime.Today.AddDays(5))
        ];
        public void AddTask(Task task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            tareasDto.Add(task);
        }
        public TaskDto GetTaskById(Guid id) => tasks.FirstOrDefault(t => t.Id == id);

         public IEnumerable<TaskDto> GetTasks() => tareasDto; // obtener todas las TareasDTO como IEnumerable
        
    }
}
