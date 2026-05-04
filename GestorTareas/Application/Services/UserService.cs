using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Domain.Enums;
using GestorDeTareas.Infrastructure.Repositories;

namespace GestorDeTareas.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) => _repository = repository;

        public List<User> GetAll() => _repository.GetAll();
        public User? GetById(int id) => _repository.GetUserById(id);
        public UserResponseDto Create(string name, string email, DateOnly birthday, bool isAdmin)
        {
            var user = new User(name, email, birthday, isAdmin);
            _repository.AddUser(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
        }
        public void Delete(int id)
        {
            var user = _repository.GetUserById(id)
            ?? throw new KeyNotFoundException($"No existe el usuario con Id {id}");
            _repository.Delete(user);
        }
    }
}
