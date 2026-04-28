using GestorDeTareas.Domain.Enums;

namespace GestorDeTareas.Domain.Entities
{
    public class NewFeature : Task
    {
        public DevelopmentArea TaskArea { get; set; }
        public NewFeature(
            string title,
            Priority priorityTask, 
            DateTime completionDate,
            DevelopmentArea area, 
            string description = null) : base(title, priorityTask, completionDate, description)
        {
            TaskArea = area;
        }

        public override string ToString()
        {
            return $"[NUEVA FUNCIONALIDAD] Título : {Title} " +
                $"- Estado: {TaskStatus} " +
                $"- Área de desarrollo: {TaskArea} " +
                $"- Fecha Creación: {CreationDate.ToString("dd/MM/yyyy")} " +
                $"- Fecha Límite: {CompletionDate} " +
                $"{(Description != null ? $"- Descripción: {Description}" : "")}";
        }
    }
}
