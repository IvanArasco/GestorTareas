using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class NewFeature : Task
    {
        public DevelopmentArea Area { get; private set; }
        public NewFeature(
            string title,
            Priority priority, 
            DateTime expirationDate,
            int userId,
            DevelopmentArea area, 
            string? description = null) : base(title, priority, expirationDate, userId, description)
        {
            Area = area;
        }

        public override string ToString()
        {
            return $"[NUEVA FUNCIONALIDAD] Título : {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Área de desarrollo: {Area} " +
                $"- Fecha Creación: {CreationDate:dd/MM/yyyy} " +
                $"- Fecha Límite: {ExpirationDate} " +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
