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

        private JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };
        public void AddTask(TaskDto nuevaTarea)
        {
            if (nuevaTarea == null) throw new ArgumentNullException(nameof(nuevaTarea));
            tareasDto.Add(nuevaTarea);
        }
        public TaskDto GetTaskById(Guid id) => tareasDto.FirstOrDefault(t => t.Id == id);

        public void Guardar(string ruta) // guardar tarea del fichero JSON
        {
            try
            {
                var json = JsonSerializer.Serialize(tareasDto, opciones);
                File.WriteAllText(ruta, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió la excepción: {ex.Message}");
            }
        }
        public void Cargar(string ruta) // recoger tarea del fichero JSON
        {
            if (!File.Exists(ruta)) return;
            try
            {
                var json = File.ReadAllText(ruta);
                tareasDto = JsonSerializer.Deserialize<List<TaskDto>>(json, opciones) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió la excepción: {ex.Message}");
            }
        }
        public void ListarTareas() // obtener todas las tareas por consola
        {
            foreach (var t in tareasDto)
            {
                Console.WriteLine($"{t.Id} | {t.Title} | {t.TaskPriority} | {t.TaskStatus}");
            }
        }
         public IEnumerable<TaskDto> GetTasks() => tareasDto; // obtener todas las TareasDTO como IEnumerable
        
    }
}
