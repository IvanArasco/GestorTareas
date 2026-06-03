using System.Text.RegularExpressions;

namespace GestorDeTareas.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; } = string.Empty;
        public string Email { get; private set; }
        public DateOnly Birthdate { get; private set; }
        public bool IsAdmin { get; private set; }
        public List<Task> Tasks { get; private set; } = new();
        public User(string username, string passwordHash, string email, DateOnly birthdate, bool isAdmin)
        {
            Username = string.IsNullOrWhiteSpace(username)
                ? throw new ArgumentException("El nombre no puede estar vacío.", nameof(username))
                : username;

            PasswordHash = passwordHash;

            Email = !VerifyEmail(email)
                ? throw new ArgumentException("Email no válido.")
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
            Username = string.IsNullOrWhiteSpace(newName)
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
            return $"[USER] Nombre : {Username} - Email: {Email} - Fecha Nacimiento: {Birthdate} - {(IsAdmin ? "Es admin" : "No es admin")}";
        }
    }
}
