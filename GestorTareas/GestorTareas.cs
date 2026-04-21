using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestorDeTareas
{
    public class GestorTareas
    {
        // lista por defecto de tareas que van a componer el JSON inicial y también utilizadas para pruebas
        private List<TareaDto> tareasDto = [
            new TareaDto("Tarea 001", TareaDto.Prioridad.Alta, DateTime.Today.AddDays(5)),
            new TareaDto("Tarea 002", TareaDto.Prioridad.Alta, DateTime.Today.AddDays(5)),
            new TareaDto("Tarea 003", TareaDto.Prioridad.Alta, DateTime.Today.AddDays(5))
        ];

        private JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };
        public void AgregarTarea(TareaDto nuevaTarea)
        {
            if (nuevaTarea == null) throw new ArgumentNullException(nameof(nuevaTarea));
            tareasDto.Add(nuevaTarea);
        }
        public TareaDto ObtenerTareaId(Guid id) => tareasDto.FirstOrDefault(t => t.Id == id);

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
                tareasDto = JsonSerializer.Deserialize<List<TareaDto>>(json, opciones) ?? new();
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
                Console.WriteLine($"{t.Id} | {t.Titulo} | {t.PrioridadTarea} | {t.EstadoTarea}");
            }
        }
         public IEnumerable<TareaDto> ObtenerTareas() => tareasDto; // obtener todas las TareasDTO como IEnumerable
        
    }
}
