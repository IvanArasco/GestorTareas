namespace GestorTareas
{
    internal class Tarea
    {
        public Guid Id { get; init; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public enum Prioridad { Baja, Media, Alta }
        public enum Estado { Pendiente, EnProgreso, Completada, Cancelada}
        public Prioridad PrioridadTarea { get; set; }
        public Estado EstadoTarea { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaLimite { get; set; }
        public string MotivoCancelacion { get; set; }
        public Tarea(string titulo, Prioridad prioridadTarea, DateTime fechaLimite, string descripcion = null)
        {
            Id = Guid.NewGuid();

            Titulo = string.IsNullOrWhiteSpace(titulo)
                ? throw new ArgumentException("El título no puede estar vacío") : titulo;

            FechaCreacion = DateTime.Today;

            FechaLimite = (fechaLimite.Date < DateTime.Today)
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : fechaLimite;

            EstadoTarea = Estado.Pendiente;

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
