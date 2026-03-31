using System.Collections;
using System.Text.RegularExpressions;

namespace GestorTareas
{
    internal class Tarea
    {
        public Guid Id { get; init; }
        public string Titulo { get; set; }
        public enum PrioridadTarea
        {
            Baja,
            Media,
            Alta
        }

        public enum EstadoTarea
        {
            Pendiente,
            EnProgreso,
            Completada,
            Cancelada
        }

        public DateTime FechaCreacion, FechaLimite { get; set; }

        private string _motivoCancelacion;

        public Tarea(string titulo, string descripcion = null, PrioridadTarea prioridadTarea, FechaLimite fechaLimite)
        {
            Guid Id = Guid.NewGuid;

            Titulo = string.IsNullOrWhiteSpace(titulo)
                ? throw new ArgumentException("El título no puede estar vacío") : titulo;

            FechaCreacion = DateTime.Now;

            FechaLimite = (fechaLimite.Date < DateTime.Today)
                ? throw new ArgumentException("La fecha límite no puede ser anterior a hoy") : fechaLimite;

            EstadoTarea = EstadoTarea.Pendiente;

            PrioridadTarea = prioridadTarea;
        }

        public void Iniciar()
        {
            EstadoTarea.EnProgreso;
        }

        public void Completar()
        {
            EstadoTarea.Completada;
        }

        public void Cancelar(string motivo)
        {
            EstadoTarea.Cancelada;
            _motivoCancelacion = motivo;
        }

        public bool EstaVencida()
        {
            return FechaLimite > DateTime.Today ? true : false;
        }

        public DateTime CalcularDiasRestantes()
        {
            return FechaLimite - DateTime.Today;
        }
        public override string ToString()
        {
            return $"Resumen - Título : {Titulo} - Estado: {EstadoTarea} Fecha Creación: {FechaCreacion} - Fecha Límite: {FechaLimite}  ";
        }
    }
}
