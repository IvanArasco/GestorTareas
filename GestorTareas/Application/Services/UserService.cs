using GestorDeTareas.Application.Dtos;
using GestorDeTareas.Domain.Entities;
using GestorDeTareas.Infrastructure.Repositories;

namespace GestorDeTareas.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) => _repository = repository;
        public List<UserResponseDto> GetAll()
        {
            return _repository.GetAll()
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Birthdate = u.Birthdate,
                    IsAdmin = u.IsAdmin
                })
                .ToList();
        }
        public UserResponseDto? GetById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Birthdate = user.Birthdate,
                IsAdmin = user.IsAdmin
            };
        }
        public UserResponseDto Create(UserRequestDto userDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var user = new User(
                userDto.Name,
                passwordHash,
                userDto.Email,
                userDto.Birthdate,
                userDto.IsAdmin);
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
