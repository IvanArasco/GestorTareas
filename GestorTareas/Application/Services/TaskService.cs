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
        public List<TaskResponseDto> GetTasksByUserId(int userId)
        {
            return _repository.GetTasksByUserId(userId)
                .Select(t => MapToResponseTaskDto(t))
                .ToList();
        }
        public TaskResponseDto Update(int id, TaskRequestDto taskDto)
        {
            // 1. Obtener la tarea del repositorio (con tracking de EF para poder guardar cambios)
            var task = _repository.GetTaskById(id)
                ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");

            // 2. Modificar propiedades comunes usando el comportamiento encapsulado de la entidad
            task.ChangeTitle(taskDto.Title);
            task.ChangeExpirationTime(taskDto.ExpirationDate);
            task.ChangePriority(taskDto.TaskPriority);

            // Al ser Description una propiedad con setter público ({ get; set; }) la editamos directamente
            task.Description = taskDto.Description;

            // 3. Modificar propiedades específicas según la subclase real de la tarea
            switch (task)
            {
                case Bug bug:
                    if (taskDto.TaskType != "Bug")
                        throw new ArgumentException("No se puede cambiar el tipo base de la tarea.");

                    bug.ActualBehaviour = taskDto.ActualBehaviour;
                    bug.ExpectedBehaviour = taskDto.ExpectedBehaviour;
                    break;

                case Improvement improvement:
                    if (taskDto.TaskType != "Improvement")
                        throw new ArgumentException("No se puede cambiar el tipo base de la tarea.");

                    improvement.AffectedFeature = taskDto.AffectedFeature;
                    improvement.ExpectedBenefict = taskDto.ExpectedBenefict;
                    break;

                case NewFeature newFeature:
                    if (taskDto.TaskType != "NewFeature")
                        throw new ArgumentException("No se puede cambiar el tipo base de la tarea.");

                    // Nota: En tu MapToResponseTaskDto usas 'task.Area', pero en tu Create usas 'taskDto.DevelopmentArea'.
                    // Ajusta esta asignación según se llame la propiedad exacta dentro de tu entidad 'NewFeature'
                    newFeature.Area = taskDto.DevelopmentArea;
                    break;

                case RecurringTask recurringTask:
                    if (taskDto.TaskType != "RecurringTask")
                        throw new ArgumentException("No se puede cambiar el tipo base de la tarea.");

                    recurringTask.Frequency = taskDto.Frequency;
                    recurringTask.LastExecution = taskDto.LastExecution;
                    recurringTask.NextExecution = taskDto.NextExecution;
                    break;

                default:
                    throw new ArgumentException("Tipo de entidad en base de datos no reconocido.");
            }

            // 4. Persistir los cambios en la base de datos a través del repositorio
            _repository.Update(task);

            // 5. Volver a recuperar de la BD para asegurar que traemos el Include del User (Username) actualizado
            var updatedTask = _repository.GetTaskById(id);

            return MapToResponseTaskDto(updatedTask!);
        }

        // Depending on which 'TaskType' field, will create different types of Tasks.
        public TaskResponseDto Create(TaskRequestDto taskDto, int UserId)
        {
            Task task = taskDto.TaskType switch
            {
                "Bug" => new Bug(
                    taskDto.Title,
                    taskDto.TaskPriority,
                    taskDto.ExpirationDate,
                    UserId,
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
                    UserId,
                    taskDto.Description
                    ),
                "NewFeature" => new NewFeature(
                    taskDto.Title,
                    taskDto.TaskPriority,
                    taskDto.ExpirationDate,
                    UserId,
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
                    UserId,
                    taskDto.Description
                ),
                _ => throw new ArgumentException("Tipo de tarea no válido.")
            };

            _repository.AddTask(task);

            // After AddTask, the entity in memory has UserId but UserName is null.
            // We get it from DB (with Include) to get the full User object and map the userName.

            var saved = _repository.GetTaskById(task.Id); 

            return MapToResponseTaskDto(saved!);
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
                UserId = task.UserId,
                Username = task.User?.Username ?? "Sin asignar",

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