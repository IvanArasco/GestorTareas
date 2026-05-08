using GestorDeTareas.Domain.Entities;

namespace GestorDeTareas.Infrastructure.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        private readonly TaskManagerContext _context;
        public UserRepositoryEF(TaskManagerContext context) => _context = context;
        public List<User> GetAll() => _context.Users.ToList();
        public User? GetUserById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);
        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
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
