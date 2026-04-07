/* identificar las diferencias de cada tarea porque esto es genérico, y si es recurrente fechas limite mas cortas etc...
Informe PDF - Excel
Tipos de tareas que usaríamos en el gestor - y qué tendrán en específico cada una a nivel de métodos y propiedades.
CalcularLimiteTiempo
*/
namespace GestorTareas
{
    public abstract class Tarea
    {
        public Guid Id { get; init; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public enum Prioridad { Baja, Media, Alta, Urgente }
        public Prioridad PrioridadTarea { get; set; }
        public enum Estado { Pendiente, EnProgreso, Completada, Cancelada }
        public Estado EstadoTarea { get; set; } = Estado.Pendiente;
        public DateTime FechaCreacion { get; set; } = DateTime.Today;
        public DateTime FechaLimite { get; set; }
        public string MotivoCancelacion { get; set; }
        public Tarea(string titulo, Prioridad prioridadTarea, DateTime fechaLimite, string descripcion = null)
        {
            Id = Guid.NewGuid();

            Titulo = string.IsNullOrWhiteSpace(titulo)
                ? throw new ArgumentException("El título no puede estar vacío") : titulo;

            FechaLimite = (fechaLimite.Date < DateTime.Today)
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : fechaLimite;

            PrioridadTarea = prioridadTarea;

            Descripcion = descripcion;
        }
        public void Iniciar() => EstadoTarea = Estado.EnProgreso;

        public void Completar() => EstadoTarea = Estado.Completada;

        public void Cancelar(string motivo)
        {
            EstadoTarea = Estado.Cancelada;
            MotivoCancelacion = motivo;
        }

        public bool EstaVencida() => FechaLimite < DateTime.Today;

        public int CalcularTiempoRestante() => (FechaLimite - DateTime.Today).Days;

        public override string ToString()
        {
            return $"Resumen - Título : {Titulo} - Estado: {EstadoTarea} - Fecha Creación: {FechaCreacion} - Fecha Límite: {FechaLimite}";
        }
    }
}
