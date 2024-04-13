using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model
{
    public enum Sectiune
    {
        STIINTE,
        TEHNOLOGIE,
        MEDICINA,
        ARTA,
        SPORT,
        TOATE,
    }

    public class Prezentare
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public int IdAutor { get; set; } 
        public string Descriere { get; set; }
        public DateTime Data { get; set; } 
        public TimeSpan Ora { get; set; } 
        public Sectiune Sectiune { get; set; }
        public int IdConferinta { get; set; } 

        // Default constructor
        public Prezentare()
        {
        }

        // Parameterized constructor 
        public Prezentare(int id, string titlu, int idAutor, string descriere, DateTime data, TimeSpan ora, Sectiune sectiune, int idConferinta)
        {
            Id = id;
            Titlu = titlu;
            IdAutor = idAutor;
            Descriere = descriere;
            Data = data;
            Ora = ora;
            Sectiune = sectiune;
            IdConferinta = idConferinta;
        }

        // Copy constructor
        public Prezentare(Prezentare prezentare)
        {
            Id = prezentare.Id;
            Titlu = prezentare.Titlu;
            IdAutor = prezentare.IdAutor;
            Descriere = prezentare.Descriere;
            Data = prezentare.Data;
            Ora = prezentare.Ora;
            Sectiune = prezentare.Sectiune;
            IdConferinta = prezentare.IdConferinta;
        }
    }
}



