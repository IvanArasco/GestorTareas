using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class Improvement : Task
    {
        public string AffectedFeature { get; set; }
        public string ExpectedBenefict { get; set; }
        public Improvement(
            string title, 
            string affectedFeature,
            string expectedBenefict,
            Priority priorityTask,
            DateTime completionDate,
            string description = null) : base(title, priorityTask, completionDate, description)
        {
            ExpectedBenefict = expectedBenefict;
            AffectedFeature = affectedFeature;
        }

        public override string ToString()
        {
            return $"[MEJORA] Título : {Title} - Estado: {TaskStatus} - Fecha Creación: {CreationDate.ToString("dd/MM/yyyy")} - Fecha Límite: {CompletionDate} - Descripción: {Description}";
        }
    }
}
