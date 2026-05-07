using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Infrastructure.Repositories;

namespace GestorDeTareas.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) => _repository = repository;
        public List<User> GetAll() => _repository.GetAll();
        public List<User> GetAllTasks() => _repository.GetAllTasks();
        public User? GetById(int id) => _repository.GetUserById(id);
        public UserResponseDto Create(UserRequestDto userDto)
        {
            var user = new User(userDto.Name, userDto.Email, userDto.Birthdate, userDto.IsAdmin);
            _repository.AddUser(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
        }
        public void Update(int id, UserRequestDto dto)
        {
            var user = _repository.GetUserById(id)
                ?? throw new KeyNotFoundException($"No existe el usuario con Id {id}");

            user.ChangeName(dto.Name);
            user.ChangeEmail(dto.Email);
            user.ChangeBirthdate(dto.Birthdate);
            user.ChangeIsAdmin(dto.IsAdmin);

            _repository.Update(user);
        }

        public void Delete(int id)
        {
            var user = _repository.GetUserById(id)
            ?? throw new KeyNotFoundException($"No existe el usuario con Id {id}");
            _repository.Delete(user);
        }
    }
}
