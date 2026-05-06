using GestorDeTareas.Application.Dtos;
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
        public List<TaskResponseDto> GetAll()
        {
            return _repository.GetAll()
                .Select(t => MapToResponseTaskDto(t))
                .ToList();
        }
        public TaskResponseDto? GetById(int id) 
        {
            var task = _repository.GetTaskById(id);
            if (task == null) return null;

            return MapToResponseTaskDto(task);
        }

        // Depending on which 'TaskType' field, will create different types of Tasks.
        public TaskResponseDto Create(TaskRequestDto taskDto)
        {
            Task task = taskDto.TaskType switch
            {
                "Bug" => new Bug(
                    taskDto.Title,
                    taskDto.TaskPriority,
                    taskDto.ExpirationDate,
                    taskDto.UserId,
                    taskDto.ActualBehaviour,
                    taskDto.ExpectedBehaviour,
                    taskDto.Description
                    ),
                "Improvement" => new Improvement(
                    taskDto.Title,
                    taskDto.AffectedFeature,
                    taskDto.ExpectedBenefict,
                    taskDto.TaskPriority,
                    taskDto.ExpirationDate,
                    taskDto.UserId,
                    taskDto.Description
                    ),
                "NewFeature" => new NewFeature(
                    taskDto.Title,
                    taskDto.TaskPriority,
                    taskDto.ExpirationDate,
                    taskDto.UserId,
                    taskDto.DevelopmentArea,
                    taskDto.Description
                    ),
                "RecurringTask" => new RecurringTask(
                    taskDto.Title,
                    taskDto.TaskPriority,
                    taskDto.ExpirationDate,
                    taskDto.Frequency,
                    taskDto.LastExecution,
                    taskDto.NextExecution,
                    taskDto.UserId,
                    taskDto.Description
                ),
                _ => throw new ArgumentException("Tipo de tarea no válido.")
            };

            _repository.AddTask(task); // en este punto mandamos la entidad, que ha sido mapeada para subirla a la BD.

            return MapToResponseTaskDto(task);
        }

        private TaskResponseDto MapToResponseTaskDto(Task task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                TaskStatus = task.TaskStatus,
                TaskPriority = task.TaskPriority,
                ExpirationDate = task.ExpirationDate,
                TaskType = task.GetType().Name,
                User = task.User?.Name ?? "Sin asignar",

                // Bug fields
                ExpectedBehaviour = task is Bug bugExpectedBehaviour ? bugExpectedBehaviour.ExpectedBehaviour : null,
                ActualBehaviour = task is Bug bugActualBehaviour ? bugActualBehaviour.ActualBehaviour : null,

                // Improvement fields
                AffectedFeature = task is Improvement impAffectedFeature ? impAffectedFeature.AffectedFeature : null,
                ExpectedBenefict = task is Improvement impExpectedBenefict ? impExpectedBenefict.ExpectedBenefict : null,

                // New feature fields
                Area = task is NewFeature nfArea ? nfArea.Area : null,

                // RecurringTask fields
                Frequency = task is RecurringTask rtFrequency ? rtFrequency.Frequency : null,
                LastExecution = task is RecurringTask rtLastExecution ? rtLastExecution.LastExecution : null,
                NextExecution = task is RecurringTask rtNextExecution ? rtNextExecution.NextExecution : null,
            };
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