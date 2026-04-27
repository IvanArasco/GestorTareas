using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public class TareaRecurrente : Tarea
    {
        public enum TipoFrecuencia { Diaria, Semanal, Mensual }
        public TipoFrecuencia Frecuencia { get; set; }
        public DateTime UltimaEjecucion { get; set; }
        public DateTime ProximaEjecucion { get; set; }

        public TareaRecurrente(
            string titulo,
            Prioridad prioridadTarea,
            DateTime fechaLimite,
            TipoFrecuencia frecuencia,
            DateTime ultimaEjecucion,
            DateTime proximaEjecucion,
            string descripcion = null) : base(titulo, prioridadTarea, fechaLimite, descripcion)
        {
            Frecuencia = frecuencia;
            UltimaEjecucion = ultimaEjecucion;

            ProximaEjecucion = ValidarFechaProximaEjecucion(proximaEjecucion)
                ? proximaEjecucion : throw new ArgumentException();
            
        }

        public override bool EstaVencida() => DateTime.Today > FechaLimite;

        public bool ValidarFechaProximaEjecucion(DateTime proximaEjecucion) => proximaEjecucion > UltimaEjecucion && FechaLimite > proximaEjecucion;

        //public void ValidarFechaUltimaEjecucion()

        // OBTENER Resumen ToSTRING [RECURRENTE] {Titulo} - Cada {IntervaloDias} dias.
    }
}
