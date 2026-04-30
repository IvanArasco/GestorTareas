using Task = GestorDeTareas.Domain.Entities.Task;

namespace GestorDeTareas.Infrastructure.Repositories

{
    public interface ITaskRepository
    {
        List<Task> GetAll();
        Task? GetTaskById(int id);
        void AddTask(Task task);
        void Update(Task task);
        void Delete(Task task);
    }
}
