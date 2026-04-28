using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class RecurringTask : Task
    {
        public Frequency TaskFrequency { get; set; }
        public DateTime? LastExecution { get; private set; }
        public DateTime NextExecution { get; private set; }

        public RecurringTask(
           string title,
            Priority priorityTask,
            DateTime completionDate,
            Frequency frecuency,
            DateTime lastExecution,
            DateTime nextExecution,
            string description = null) : base(title, priorityTask, completionDate, description)
        {
            TaskFrequency = frecuency;
            LastExecution = lastExecution;

            NextExecution = ValidateNextExecutionDate(nextExecution)
                ? nextExecution : throw new ArgumentException();
        }

        public override bool HasExpired() => DateTime.Today > CompletionDate;

        public bool ValidateNextExecutionDate(DateTime nextExecution) => nextExecution > LastExecution && CompletionDate > nextExecution;

        public override string ToString()
        {
            return $"[RECURRENTE] Título : {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Frecuencia: {TaskFrequency} " +
                $"- Próxima ejecución: {NextExecution} " +
                $"{(LastExecution != null ? $"- Última ejecución: {LastExecution} " : "")}" +
                $"- Fecha Creación: {CreationDate.ToString("dd/MM/yyyy")} " +
                $"- Fecha Límite: {CompletionDate} " +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }

        //public void ValidarFechaUltimaEjecucion()

        // OBTENER Resumen ToSTRING [RECURRENTE] {Titulo} - Cada {IntervaloDias} dias.
    }
}
