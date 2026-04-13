using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public class NuevaFuncionalidad : Tarea
    {
        public enum AreaDesarrollo {Frontend, Backend, BaseDatos}
        public AreaDesarrollo Area { get; set; }
        public NuevaFuncionalidad(
            string titulo,
            Prioridad prioridadTarea, 
            DateTime fechaLimite,
            AreaDesarrollo areaAfectacion, 
            string descripcion = null) : base(titulo, prioridadTarea, fechaLimite, descripcion)
        {
            Area = areaAfectacion;
        }
    }
}
