using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;
using GestorDeTareas.Infrastructure.Repositories;
using Task = GestorDeTareas.Domain.Entities.Task;

namespace GestorDeTareas.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;
        public TaskService(ITaskRepository repository) => _repository = repository;
        public List<Task> GetAll() => _repository.GetAll();
        public Task? GetById(int id) => _repository.GetTaskById(id);
        public Task Create(string title, Priority priority, DateTime expirationDate, int userId, DevelopmentArea developmentArea)
        {
            // Validación de negocio — no pertenece al controller
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("El título no puede estar vacío");
            var task = new NewFeature(title, priority, expirationDate, userId, developmentArea);
            _repository.AddTask(task);
            return task;
        }

        public void Start(int id)
        {
            var task = _repository.GetTaskById(id)
                ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");
            task.Start();
            _repository.Update(task);
        }

        public void Complete(int id)
        {
            var task = _repository.GetTaskById(id)
                ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");
            task.Complete();
            _repository.Update(task);
        }

        public void Cancel(int id, string reason)
        {
            var task = _repository.GetTaskById(id)
                ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");
            task.Cancel(reason);
            _repository.Update(task);
        }

        public void ChangePriority(int id, Priority newPriority)
        {
            var task = _repository.GetTaskById(id)
                ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");
            task.ChangePriority(newPriority);
            _repository.Update(task);
        }

        public void Delete(int id)
        {
            var task = _repository.GetTaskById(id)
                ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");
            _repository.Delete(task);
        }
    }
}