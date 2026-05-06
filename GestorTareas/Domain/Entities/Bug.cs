using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities

{
    public class Bug : Task
    {
        public string? ExpectedBehaviour { get; private set; }
        public string? ActualBehaviour { get; private set; }
        public Bug(
            string title,
            Priority priority,
            DateTime expirationDate,
            int userId,
            string? actualBehaviour = null,
            string? expectedBehaviour = null,
            string? description = null
            ) : base(
                title,
                priority,
                expirationDate,
                userId,
                description)
        {
            ExpectedBehaviour = expectedBehaviour;
            ActualBehaviour = actualBehaviour;
        }
        public override string ToString()
        {
            return $"[BUG] Título: {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Fecha Creación: {CreationDate:dd/MM/yyyy} " +
                $"- Fecha Límite: {ExpirationDate} " +
                $"{(ActualBehaviour != null ? $"- Comportamiento actual: {ActualBehaviour} " : "")} " +
                $"{(ExpectedBehaviour != null ? $"- Comportamiento esperado: {ExpectedBehaviour} " : "")} " +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
