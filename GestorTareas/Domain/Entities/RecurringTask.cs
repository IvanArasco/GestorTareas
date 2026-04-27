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
            Frequency frecuencia,
            DateTime lastExecution,
            DateTime nextExecution,
            string description = null) : base(title, priorityTask, completionDate, description)
        {
            TaskFrequency = frecuencia;
            LastExecution = lastExecution;

            NextExecution = ValidateNextExecutionDate(nextExecution)
                ? nextExecution : throw new ArgumentException();
        }

        public override bool HasExpired() => DateTime.Today > CompletionDate;

        public bool ValidateNextExecutionDate(DateTime nextExecution) => nextExecution > LastExecution && CompletionDate > nextExecution;

        //public void ValidarFechaUltimaEjecucion()

        // OBTENER Resumen ToSTRING [RECURRENTE] {Titulo} - Cada {IntervaloDias} dias.
    }
}
