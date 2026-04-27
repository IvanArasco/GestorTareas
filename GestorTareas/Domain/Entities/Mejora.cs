using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class Mejora : Tarea
    {
        public string FuncionalidadAfectada { get; set; }
        public string BeneficioEsperado { get; set; }
        public Mejora(
            string titulo, 
            string funcionalidadAfectada,
            string beneficioEsperado,
            Prioridad prioridadTarea,
            DateTime fechaLimite,
            string descripcion = null) : base(titulo, prioridadTarea, fechaLimite, descripcion)
        {
            BeneficioEsperado = beneficioEsperado;
            FuncionalidadAfectada = funcionalidadAfectada;
        }
    }
}
