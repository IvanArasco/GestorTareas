using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class Improvement : Task
    {
        public string? AffectedFeature { get; private set; }
        public string? ExpectedBenefict { get; private set; }
        public Improvement(
            string title,
            string? affectedFeature,
            string? expectedBenefict,
            Priority taskPriority,
            DateTime expirationDate,
            int userId,
            string? description = null) : base(title, taskPriority, expirationDate, userId, description)
        {
            ExpectedBenefict = expectedBenefict;
            AffectedFeature = affectedFeature;
        }

        public void ChangeExpectedBenefict(string? expectedBenefict)
        {
            ExpectedBenefict = expectedBenefict;
        }

        public void ChangeAffectedFeature(string? affectedFeature)
        {
            AffectedFeature = affectedFeature;
        }

        public override string ToString()
        {
            return $"[MEJORA] Título : {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Fecha Creación: {CreationDate:dd/MM/yyyy} " +
                $"- Fecha Límite: {ExpirationDate:dd/MM/yyyy} " +
                $"{(AffectedFeature != null ? $"- Característica afectada: {AffectedFeature} " : "")}" +
                $"{(ExpectedBenefict != null ? $"- Beneficio esperado: {ExpectedBenefict} " : "" )}" +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
