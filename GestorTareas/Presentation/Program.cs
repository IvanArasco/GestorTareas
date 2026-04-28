using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;
using Task = GestorDeTareas.Domain.Entities.Task;

public class Program
{
    public static void Main(string[] args)
    {
        List<Task> tareas = new List<Task>(capacity: 30); // 30 tareas aprox

        TaskManager gestor = new TaskManager();
        GenerateTasks(tareas);
        foreach (Task task in tareas)
        {
            Console.WriteLine(task.ToString());
        }

    }
    private static void GenerateTasks(List<Task> tareas)
    {
        for (int i = 0; i < 5; i++) // Añadir Bugs
        {
            tareas.Add(new Bug(
                $"Bug 00{i}",
                Priority.Medium,
                DateTime.Now.AddDays(5),
                $"ComportamientoActual 00{i}",
                $"ComportamientoEsperado 00{i}"
               )
            );
        }

        for (int i = 0; i < 5; i++) // Añadir Mejoras
        {
            tareas.Add(new Improvement(
                $"Mejora 00{i}",
                $"Funcionalidad afectada 00{i}",
                $"Cambio esperado 00{i}",
                Priority.Medium,
                DateTime.Now.AddDays(4)
               )
            );
        }

        for (int i = 0; i < 5; i++) // Añadir Nuevas funcionalidades
        {
            tareas.Add(new NewFeature(
                $"Nueva funcionalidad 00{i}",
                Priority.High,
                DateTime.Now.AddDays(3),
                DevelopmentArea.Backend
               )
            );
        }

        for (int i = 0; i < 5; i++) // Añadir Tareas recurrentes
        {
            tareas.Add(new RecurringTask(
                $"Tarea recurrente 00{i}",
                Priority.Low,
                DateTime.Now.AddDays(8),
                Frequency.Weekly,
                DateTime.Now,
                DateTime.Now.AddDays(4)
               )
            );
        }
    }
}

/* gestor.ListarTareas();

gestor = new GestorTareas();
gestor.Cargar("./tareas.json");
gestor.ListarTareas();

GestorTareas gestor = new GestorTareas();
gestor.Guardar("./tareas.json");
gestor.ListarTareas();
*/
