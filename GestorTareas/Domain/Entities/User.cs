using static System.Net.WebRequestMethods;

namespace GestorDeTareas.Domain.Entities
{
    internal class User
    {
        public string Name {  get; private set; }
        public DateTime Birthdate { get; private set; }
        public bool IsAdmin { get; private set; }

        public User(string name, DateTime birthdate, bool isAdmin)
        {
            Name = name;
            Birthdate = birthdate;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return $"[USER] Nombre : {Name} - Fecha Nacimiento: {Birthdate} - {(IsAdmin ? "Es admin" : "No es admin")}";
        }
    }
}
