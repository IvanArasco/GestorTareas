using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;
using GestorDeTareas.Infrastructure.Repositories;

namespace GestorDeTareas.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;
        public TaskService(ITaskRepository repository) => _repository = repository;
        public List<TaskResponseDto> GetAll() // Mapear para el DTO
        {
            return _repository.GetAll()
                .Select(t => new TaskResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    TaskStatus = t.TaskStatus,
                    TaskPriority = t.Priority,
                    ExpirationDate = t.ExpirationDate,
                    User = t.User?.Name ?? "Sin asignar"
                })
                .ToList();
        }
        public TaskResponseDto? GetById(int id) // Mapear para el DTO
        {
            var task = _repository.GetTaskById(id);
            if (task == null) return null;

            return new TaskResponseDto 
            {
                Id = task.Id,
                Title = task.Title,
                TaskStatus = task.TaskStatus,
                TaskPriority = task.Priority,
                ExpirationDate = task.ExpirationDate,
                User = task.User?.Name ?? "Sin asignar"
            };
        }

        /* TO DO : DTO FOR EACH CHILD CLASS ???
         * 
        public TaskResponseDto Create(string title, Priority priority, DateTime expirationDate, int userId, DevelopmentArea developmentArea)
        {
            Task task = dto.TipoTarea switch
            {
                "Bug" => new Bug(...),
                "Improvement" => new Improvement(...),
                "NewFeature" => new NewFeature(...),
                _ => throw new ArgumentException("Tipo de tarea no válido")
            };

            var task = new TaskResponseDto(title, priority, expirationDate, userId, developmentArea);
            _repository.AddTask(task);
            return task;
        }
        */
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