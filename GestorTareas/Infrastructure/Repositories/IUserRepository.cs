using GestorDeTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        List<User> GetAllTasks();
        User? GetUserById(int id);
        void AddUser(User user);
        void Update(User user);
        void Delete(User user);
    }

}
