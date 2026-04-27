namespace GestorDeTareas.Domain.Entities
{
    public class User
    {
        public string Name {  get; private set; }
        public string Email { get; private set; }
        public DateTime Birthdate { get; private set; }
        public bool IsAdmin { get; private set; }

        public User(string name, string email, DateTime birthdate, bool isAdmin)
        {
            Name = name;
            Email = email;
            Birthdate = birthdate;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return $"[USER] Nombre : {Name} - Email: {Email} - Fecha Nacimiento: {Birthdate} - {(IsAdmin ? "Es admin" : "No es admin")}";
        }
    }
}
