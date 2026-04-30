using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Infrastructure.Repositories;
using Task = GestorDeTareas.Domain.Entities.Task;

namespace GestorDeTareas.Application.Services
{
    public class TaskManagerService
    {
        private readonly ITaskRepository _repository;

        public TaskManagerService(ITaskRepository repositorio)
        => _repository = repositorio;

        public List<Task> ObtenerTodas() => _repository.GetAll();
        public Task? ObtenerPorId(Guid id) => _repository.GetTaskById(id);

        public Task Create(string title, DateTime? expirationDate, int userId)
        {
            // Validación de negocio — no pertenece al controller
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("El título no puede estar vacío");
            var task = new Bug();
            /*
            var tarea = new TareaSimple
            { Titulo = titulo, FechaLimite = fechaLimite, UsuarioId = usuarioId };
            _repositorio.Agregar(tarea);
            */
            return task;
        }

        public void Completar(int id)
        {
            var tarea = _repository.ObtenerPorId(id)
            ?? throw new KeyNotFoundException($"No existe la tarea con Id {id}");
            tarea.EstaCompletada = true;
            _repository.Actualizar(tarea);
        }
    }
}
