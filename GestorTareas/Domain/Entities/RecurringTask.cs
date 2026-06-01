using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class RecurringTask : Task
    {
        public Frequency? Frequency { get; private set; }
        public DateTime? LastExecution { get; private set; }
        public DateTime? NextExecution { get; private set; }
        public RecurringTask(
           string title,
            Priority taskPriority,
            DateTime expirationDate,
            Frequency? frequency,
            DateTime? lastExecution,
            DateTime? nextExecution,
            int userId,
            string? description = null) : base(title, taskPriority, expirationDate, userId, description)
        {
            Frequency = frequency;
            LastExecution = lastExecution;

            NextExecution = ValidateNextExecutionDate(nextExecution)
                ? nextExecution 
                : throw new ArgumentException("La próxima ejecución no puede ser posterior a la fecha de expiración o anterior a la última ejecución.");
        }

        public void ChangeFrequency(Frequency? freq)
        {
            Frequency = freq;
        }
        public void ChangeLastExecution(DateTime? lastExecution)
        {
            LastExecution = lastExecution;
        }

        public void ChangeNextExecution(DateTime? nextExecution)
        {
            if (!ValidateNextExecutionDate(nextExecution))
                throw new ArgumentException("La próxima ejecución no puede ser posterior a la fecha de expiración o anterior a la última ejecución.");

            NextExecution = nextExecution;
        }

        public bool ValidateNextExecutionDate(DateTime? nextExecution) => (
            !LastExecution.HasValue || nextExecution > LastExecution) && ExpirationDate > nextExecution;
        public override string ToString()
        {
            return $"[RECURRENTE] Título : {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Frecuencia: {Frequency} " +
                $"{(NextExecution.HasValue ? $"- Próxima ejecución: {NextExecution} " : "")}" +
                $"{(LastExecution.HasValue ? $"- Última ejecución: {LastExecution} " : "")}" +
                $"- Fecha Creación: {CreationDate:dd/MM/yyyy} " +
                $"- Fecha Límite: {ExpirationDate} " +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
