using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class Improvement : Task
    {
        public string AffectedFeature { get; private set; }
        public string? ExpectedBenefict { get; private set; }
        public Improvement(
            string title,
            string affectedFeature,
            string? expectedBenefict,
            Priority priority,
            DateTime completionDate,
            string? description = null) : base(title, priority, completionDate, description)
        {
            ExpectedBenefict = expectedBenefict;
            AffectedFeature = affectedFeature;
        }

        public override string ToString()
        {
            return $"[MEJORA] Título : {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Fecha Creación: {CreationDate:dd/MM/yyyy} " +
                $"- Fecha Límite: {CompletionDate:dd/MM/yyyy} " +
                $"- Característica afectada: {AffectedFeature} " +
                $"{(ExpectedBenefict != null ? $"- Beneficio esperado: {ExpectedBenefict} " : "" )}" +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
