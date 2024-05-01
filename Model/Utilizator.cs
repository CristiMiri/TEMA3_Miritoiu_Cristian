using System;
using System.Security.Cryptography;
using System.Text;
namespace PS_TEMA3.Model
{
    public enum UserType
    {
        PARTICIPANT,
        ORGANIZATOR,
        ADMINISTRATOR
    }

    public class Utilizator
    {
        // Simplified property declarations for all attributes
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public UserType UserType { get; set; }
        public string Telefon { get; set; }
        
        public Utilizator(int id, string nume, string email, string parola, UserType userType, string telefon)
        {
            Id = id;
            Nume = nume;
            Email = email;
            Parola = HashPassword(parola); // Ensuring password is hashed when set
            UserType = userType;
            Telefon = telefon;
        }

        public Utilizator(Utilizator utilizator)
            : this(utilizator.Id, utilizator.Nume, utilizator.Email, utilizator.Parola, utilizator.UserType, utilizator.Telefon)
        {
        }

        public Utilizator()
        {
            Id = 0;
            Nume = "";
            Email = "";
            Parola = "";
            UserType = UserType.PARTICIPANT;
            Telefon = "";
        }

        public Utilizator(string email, string parola)
            : this(0, "", email, parola, UserType.PARTICIPANT, "")
        {
        }

        // Password hashing function to ensure password security.
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
       
        public override string ToString()
        {
            return $"Utilizatorul cu id-ul {Id} si numele {Nume} are email-ul {Email} si este de tipul {UserType} si are numarul de telefon {Telefon}.";
        }
    }
}
