using System.Text.RegularExpressions;

namespace GestorDeTareas.Domain.Entities
{
    public class User
    {
        public int Id { get; init; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateOnly Birthdate { get; private set; }
        public bool IsAdmin { get; private set; }
        public ICollection<Task> Tasks { get; private set; } = new List<Task>();
        public User(string name, string email, DateOnly birthdate, bool isAdmin)
        {

            Name = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("El nombre no puede estar vacío.", nameof(name))
            : name;
            Email = ValidarEmail(email) ? email : throw new InvalidOperationException("Error creando el mail");
            Birthdate = birthdate;
            IsAdmin = isAdmin;
        }
        private static bool ValidarEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            }
            return false;

        }
        public override string ToString()
        {
            return $"[USER] Nombre : {Name} - Email: {Email} - Fecha Nacimiento: {Birthdate} - {(IsAdmin ? "Es admin" : "No es admin")}";
        }
    }
}
