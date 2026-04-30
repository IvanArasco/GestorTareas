using GestorDeTareas.Domain.Entities;

namespace GestorDeTareas.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetUserById(int id);
        void AddUser(User user);
        void Update(User user);
        void Delete(User user);
    }

}
}
