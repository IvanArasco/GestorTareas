using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;
using Task = GestorDeTareas.Domain.Entities.Task;

List<Task> tareas = new List<Task>(capacity: 30); // 30 tareas aprox

TaskManager gestor = new TaskManager();

Random rnd = new Random();

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
        DateTime.Now.AddDays(5)
       )
    );
}

//gestor.ListarTareas();
/*
 * Tarea bug = new Bug("No se ve el botón", Tarea.Prioridad.Alta, DateTime.Now.AddDays(5), "No se ve el botón", "Visualizar el botón");
bug.Descripcion = "Ya se ve pero va regulín";
Console.WriteLine(bug);

gestor = new GestorTareas();
gestor.Cargar("./tareas.json");
gestor.ListarTareas();

GestorTareas gestor = new GestorTareas();
gestor.Guardar("./tareas.json");
gestor.ListarTareas();
*/
