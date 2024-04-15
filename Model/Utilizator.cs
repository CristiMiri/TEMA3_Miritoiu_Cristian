using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public UserType UserType { get; set; }
        public string Telefon { get; set; }
        

        public Utilizator(int id, string nume, string email, string parola, UserType userType, string telefon)
        {
            this.Id = id;
            this.Nume = nume;
            this.Email = email;
            this.Parola = parola;
            this.UserType = userType;
            this.Telefon = telefon;
        }

        public Utilizator(Utilizator utilizator)
        {
            this.Id = utilizator.Id;
            this.Nume = utilizator.Nume;
            this.Email = utilizator.Email;
            this.Parola = utilizator.Parola;
            this.UserType = utilizator.UserType;
            this.Telefon = utilizator.Telefon;
        }

        public Utilizator()
        {            
            this.Id = 0;
            this.Nume = "";
            this.Email = "";
            this.Parola = "";
            this.UserType = UserType.PARTICIPANT;
            this.Telefon = "";
        }

        public Utilizator(string email, string password)
        {            
            this.Email = email;
            this.Parola = password;
            this.Id = 0;
            this.Nume = "";
            this.UserType = UserType.PARTICIPANT;
            this.Telefon = "";
        }


        public override string ToString()
        {
            return "Utilizatorul cu id-ul " + Id + " si numele " + Nume + " are email-ul " + Email + " si parola " + Parola + " si este de tipul " + UserType + " si are numarul de telefon " + Telefon + ".";
        }
    }
}
