namespace GestorDeTareas.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        List<Task> GetAll();
        Task? GetTaskById(Guid id);
        void AddTask(Task task);
        void Update(Task task);
        void Delete(Task task);
    }
}
