using GestorDeTareas.Domain.Entities;

namespace GestorDeTareas.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetUserById(int id);
        User? GetByEmail(string email);
        void AddUser(User user);
        void Update(User user);
        void Delete(User user);
    }

}
