using Microsoft.EntityFrameworkCore;
using Task = GestorDeTareas.Domain.Entities.Task;

namespace GestorDeTareas.Infrastructure.Repositories
{
    public class TaskRepositoryEF : ITaskRepository
    {
        private readonly TaskManagerContext _context;

        public TaskRepositoryEF(TaskManagerContext context)
        => _context = context;

        public List<Task> GetAll() => _context.Tasks.Include(t => t.User).ToList();
        
        public Task? GetTaskById(int id) => _context.Tasks.Include(t => t.User).FirstOrDefault(t => t.Id == id);

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }
        public void Update(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
