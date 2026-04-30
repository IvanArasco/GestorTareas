using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class RecurringTask : Task
    {
        public Frequency Frequency { get; private set; }
        public DateTime? LastExecution { get; private set; }
        public DateTime NextExecution { get; private set; }
        public RecurringTask(
           string title,
            Priority priority,
            DateTime completionDate,
            Frequency frequency,
            DateTime? lastExecution,
            DateTime nextExecution,
            string? description = null) : base(title, priority, completionDate, description)
        {
            Frequency = frequency;
            LastExecution = lastExecution;

            NextExecution = ValidateNextExecutionDate(nextExecution)
                ? nextExecution : throw new ArgumentException();
        }

        public bool ValidateNextExecutionDate(DateTime nextExecution) => nextExecution > LastExecution && ExpirationDate > nextExecution;

        public override string ToString()
        {
            return $"[RECURRENTE] Título : {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Frecuencia: {Frequency} " +
                $"- Próxima ejecución: {NextExecution} " +
                $"{(LastExecution.HasValue ? $"- Última ejecución: {LastExecution} " : "")}" +
                $"- Fecha Creación: {CreationDate:dd/MM/yyyy} " +
                $"- Fecha Límite: {ExpirationDate} " +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
