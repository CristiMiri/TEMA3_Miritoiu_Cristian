using System;

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
        public string Descriere { get; set; }
        public DateTime Data { get; set; } 
        public TimeSpan Ora { get; set; } 
        public Sectiune Sectiune { get; set; }
        public int IdConferinta { get; set; }
        public int IdAutor { get; set; }

        public Prezentare()  // Default constructor initializes default values
        {
            Id = 0;
            Titlu = "";            
            Descriere = "";
            Data = DateTime.Today;  
            Ora = TimeSpan.Zero;  
            Sectiune = Sectiune.TOATE;
            IdConferinta = 0;
            IdAutor = 0;
        }

        public Prezentare(int id, string titlu, string descriere, DateTime data, TimeSpan ora, Sectiune sectiune, int idConferinta,int IdAutor)
        {
            Id = id;
            Titlu = titlu;            
            Descriere = descriere;
            Data = data;
            Ora = ora;
            Sectiune = sectiune;
            IdConferinta = idConferinta;
            IdAutor = IdAutor;
        }

        public Prezentare(Prezentare prezentare)  // Copy constructor
        {
            Id = prezentare.Id;
            Titlu = prezentare.Titlu;            
            Descriere = prezentare.Descriere;
            Data = prezentare.Data;
            Ora = prezentare.Ora;
            Sectiune = prezentare.Sectiune;
            IdConferinta = prezentare.IdConferinta;
            IdAutor = prezentare.IdAutor;
        }
    }
}
