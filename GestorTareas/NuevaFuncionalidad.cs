using System;
using System.Collections.Generic;
using System.Text;

namespace GestorTareas
{
    internal class NuevaFuncionalidad : Tarea
    {
        public enum AreaAfectacion {Frontend, Backend, BaseDatos}
        public AreaAfectacion Area { get; set; }
        public NuevaFuncionalidad(
            string titulo, 
            Prioridad prioridadTarea, 
            DateTime fechaLimite, 
            AreaAfectacion areaAfectacion, 
            string descripcion = null) : base(titulo, prioridadTarea, fechaLimite, descripcion)
        {
            Area = areaAfectacion;
        }
    }
}
