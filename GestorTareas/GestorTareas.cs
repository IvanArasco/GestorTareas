using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestorDeTareas
{
    internal class GestorTareas
    {
        private List<TareaDto> tareas = [
            new TareaDto("Tarea 001", TareaDto.Prioridad.Alta,DateTime.Today.AddDays(5)),
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

        public void Guardar(string ruta)
        {

            try
            {
                var json = JsonSerializer.Serialize(tareas, opciones);
                File.WriteAllText(ruta, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió la excepción: {ex.Message}");
            }
        }
        public void Cargar(string ruta)
        {
            if (!File.Exists(ruta)) return;
            try
            {
                var json = File.ReadAllText(ruta);
                tareas = JsonSerializer.Deserialize<List<TareaDto>>(json, opciones) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió la excepción: {ex.Message}");
            }
        }
        public void ListarTareas()
        {
            foreach (var t in tareas)
            {
                Console.WriteLine($"{t.Id} | {t.Titulo} | {t.PrioridadTarea} | {t.EstadoTarea}");
            }
        }

        public void MostrarResumen(IEnumerable<Tarea> tareas)
        {
            int total = 0;
            int vencidas = 0;

            foreach (Tarea tarea in tareas)
            {
                // ObtenerResumen() polimórfico:
                // el runtime elige la versión correcta según el tipo real
                //Console.WriteLine(tarea.ObtenerResumen());
                // total++;
                //if (tarea.EstaVencida) vencidas++;
            }

            // Console.WriteLine($"\nTotal: {total} tareas · Vencidas: {vencidas}");
        }

        /*
         public override string ObtenerResumen() =>
            $"[URGENTE] {Titulo} | Responsable: {Responsable} " +
            $"| Límite: {FechaLimiteHora:dd/MM/yy HH:mm} | {Estado}" +
            (EstaVencidaPorHoras ? " [VENCIDA]" : "");
        }
         */
    }
}
