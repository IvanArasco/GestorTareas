using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities

{
    public class Bug : Task
    {
        public string ExpectedBehaviour { get; set; }
        public string ActualBehaviour { get; set; }
        public Bug(
            string title,
            Priority taskpriority,
            DateTime completiondate,
            string actualbehaviour,
            string expectedbehaviour = null,
            string description = null
            ) : base(
                title,
                taskpriority,
                completiondate,
                description ?? $"Comportamiento actual: {actualbehaviour} | Comportamiento esperado: {expectedbehaviour}")
        {
            this.ExpectedBehaviour = expectedbehaviour;
            this.ActualBehaviour = actualbehaviour;
        }

        public override string ToString()
        {
            return $"[BUG] Título : {Title} - Estado: {TaskStatus} - Fecha Creación: {CreationDate.ToString("dd/MM/yyyy")} - Fecha Límite: {CompletionDate} - Descripción: {Description}";
        }
    }
}
