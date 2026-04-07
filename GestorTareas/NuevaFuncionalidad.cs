using System;
using System.Collections.Generic;
using System.Text;

namespace GestorTareas
{
    internal class NuevaFuncionalidad : Tarea
    {
        public NuevaFuncionalidad(string titulo, Prioridad prioridadTarea, DateTime fechaLimite, string descripcion = null) : base(titulo, prioridadTarea, fechaLimite, descripcion)
        {
        }
    }
}
