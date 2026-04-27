namespace GestorDeTareas.Application.Dtos
{
    public class TareaDto // guardar y recuperar los datos de la Tarea => Intermediario entre capa de negocio / lógica y frontend.
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; }
        public enum Prioridad { Baja, Media, Alta, Urgente }
        public Prioridad PrioridadTarea { get; set; }
        public enum Estado { Pendiente, EnProgreso, Completada, Cancelada }
        public Estado EstadoTarea { get; set; } = Estado.Pendiente;
        public DateTime FechaLimite { get; set; }
        public TareaDto() { }
        public TareaDto(string titulo, Prioridad prioridadTarea, DateTime fechaLimite)
        {
            Titulo = titulo;
            PrioridadTarea = prioridadTarea;
            FechaLimite = fechaLimite;
        }
    }
}
