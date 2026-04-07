using GestorTareas;

Tarea bug = new Bug("No se ve el botón", Tarea.Prioridad.Alta, DateTime.Now.AddDays(5), "No se ve el botón", "Visualizar el botón");
bug.Descripcion = "Ya se ve pero va regulín";
Console.WriteLine(bug);