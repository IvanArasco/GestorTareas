namespace GestorDeTareas.Domain.Entities
{
    public class User
    {
        public Guid Id { get; init; }
        public string Name {  get; private set; }
        public string Email { get; private set; }
        public DateOnly Birthdate { get; private set; }
        public bool IsAdmin { get; private set; }

        public User(string name, string email, DateOnly birthdate, bool isAdmin)
        {
            Id = Guid.NewGuid();
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
