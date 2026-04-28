using GestorDeTareas.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace GestorDeTareas.Domain.Entities

{
    public class Bug : Task
    {
        public string? ExpectedBehaviour { get; private set; }
        public string ActualBehaviour { get; private set; }
        public Bug(
            string title,
            Priority taskpriority,
            DateTime completiondate,
            string actualbehaviour,
            string? expectedbehaviour = null,
            string? description = null
            ) : base(
                title,
                taskpriority,
                completiondate,
                description)
        {
            ExpectedBehaviour = expectedbehaviour;
            ActualBehaviour = actualbehaviour;
        }
        public override string ToString()
        {
            return $"[BUG] Título: {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Fecha Creación: {CreationDate:dd/MM/yyyy} " +
                $"- Fecha Límite: {CompletionDate} " +
                $"- Comportamiento actual: {ActualBehaviour} " +
                $"{(ExpectedBehaviour != null ? $"- Comportamiento esperado: {ExpectedBehaviour} " : "")} " +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
