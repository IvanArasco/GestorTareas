using GestorDeTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Infrastructure.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        private readonly TaskManagerContext _context;
        public UserRepositoryEF(TaskManagerContext context) => _context = context;

        public List<User> GetAll() => _context.Users.ToList();
        public List<User> GetAllTasks() => _context.Users.Include(u => u.Tasks).ToList();
        public User? GetUserById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
