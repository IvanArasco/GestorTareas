using GestorDeTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        List<User> GetAllTasks();
        User? GetUserById(int id);
        User? GetByEmail(string email);
        void AddUser(User user);
        void Update(User user);
        void Delete(User user);
    }

}
