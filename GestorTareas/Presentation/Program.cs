using GestorDeTareas.Domain.Entities;

List<Tarea> tareas = new List<Tarea>(capacity: 30); // 30 tareas aprox

GestorTareas gestor = new GestorTareas();

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
