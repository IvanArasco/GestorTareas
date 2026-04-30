using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;
using GestorDeTareas.Infrastructure.Repositories;
using Task = GestorDeTareas.Domain.Entities.Task;

namespace GestorDeTareas.Application.Services
{
    public class TaskManagerService
    {
        private readonly ITaskRepository _repository;

        public TaskManagerService(ITaskRepository repositorio)
        => _repository = repositorio;
        public List<Task> GetAll() => _repository.GetAll();
        public Task? GetById(Guid id) => _repository.GetTaskById(id);

        public Task Create(string title, DateTime? expirationDate, int userId)
        {
            // Validación de negocio — no pertenece al controller
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("El título no puede estar vacío");
            var task = new NewFeature("Modo oscuro", Priority.Low, DateTime.Today.AddDays(5), DevelopmentArea.Frontend);
            _repository.AddTask(task);
            return task;
        }

        public void Complete(Guid id)
        {
            var task = _repository.GetTaskById(id)
            ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");
            task.Complete();
            _repository.Update(task);
        }
    }
}
