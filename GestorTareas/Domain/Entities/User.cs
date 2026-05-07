using System.Text.RegularExpressions;

namespace GestorDeTareas.Domain.Entities
{
    public class User
    {
        public int Id { get; init; }
        public string Name { get; private set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; private set; }
        public DateOnly Birthdate { get; private set; }
        public bool IsAdmin { get; private set; }
        public List<Task> Tasks { get; private set; } = new();
        public User(string name, string email, DateOnly birthdate, bool isAdmin)
        {

            Name = string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("El nombre no puede estar vacío.", nameof(name))
                : name;
            Email = !VerifyEmail(email)
                ? throw new InvalidOperationException("Email no válido.")
                : email;
            Birthdate = !VerifyBirthdate(birthdate)
                ? throw new ArgumentException("La fecha de nacimiento no puede ser futura.")
                : birthdate;
            IsAdmin = isAdmin;
        }
        public bool VerifyEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }
        public bool VerifyBirthdate(DateOnly birthdate)
        {
            return birthdate <= DateOnly.FromDateTime(DateTime.Today);
        }
        public void ChangeName(string newName)
        {
            Name = string.IsNullOrWhiteSpace(newName)
                ? throw new ArgumentException("El nombre no puede estar vacío.")
                : newName;
        }

        public void ChangeEmail(string newEmail)
        {
            Email = !VerifyEmail(newEmail)
                ? throw new InvalidOperationException("Email no válido.")
                : newEmail;
        }

        public void ChangeBirthdate(DateOnly newBirthdate)
        {
            Birthdate = !VerifyBirthdate(newBirthdate)
                ? throw new InvalidOperationException("Fecha no válida. No puede superar la de hoy.")
                : newBirthdate;
        }
        public void ChangeIsAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return $"[USER] Nombre : {Name} - Email: {Email} - Fecha Nacimiento: {Birthdate} - {(IsAdmin ? "Es admin" : "No es admin")}";
        }
    }
}
