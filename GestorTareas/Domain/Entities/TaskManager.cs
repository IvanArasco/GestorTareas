using GestorDeTareas.Application.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;
using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class TaskManager
    {
        private List<TaskDto> tareasDto = [
            new TaskDto("Tarea 001", Priority.High, DateTime.Today.AddDays(5)),
            new TaskDto("Tarea 002", Priority.High, DateTime.Today.AddDays(5)),
            new TaskDto("Tarea 003", Priority.High, DateTime.Today.AddDays(5))
        ];
 
         public IEnumerable<TaskDto> GetTasks() => tareasDto; // obtener todas las TareasDTO como IEnumerable
        
    }
}
