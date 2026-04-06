namespace GestorTareas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tarea tarea = new Tarea("Fregar los platos", Tarea.Prioridad.Baja, DateTime.Now.AddDays(2));
            Console.WriteLine(tarea);

            Console.WriteLine($"Tiempo restante de la tarea: {tarea.CalcularTiempoRestante()}");

            tarea.Cancelar("Se ocupa otra persona.");

            Console.WriteLine($"El estado de la tarea es: {tarea.EstadoTarea}");

            Console.Write($"¿Está vencida la tarea? {tarea.EstaVencida()}");
        }
    }
}
