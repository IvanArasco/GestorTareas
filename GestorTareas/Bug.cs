using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GestorTareas
{
    internal class Bug : Tarea
    {
        public string ComportamientoEsperado { get; set; }
        public string ComportamientoActual { get; set; }
        public Bug(
            string titulo,
            Prioridad prioridadTarea,
            DateTime fechaLimite,
            string comportamientoActual,
            string comportamientoEsperado = null,
             string descripcion = null
            ) : base(
                titulo,
                prioridadTarea,
                fechaLimite,
                descripcion ?? $"Comportamiento actual: {comportamientoActual} | Comportamiento esperado: {comportamientoEsperado}")
        {
            ComportamientoEsperado = comportamientoEsperado;
            ComportamientoActual= comportamientoActual;
        }
    }
}
