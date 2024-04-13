using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model
{
    public class Participant
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string CNP { get; set; }
        public string PdfFilePath { get; set; }
        public int IdPrezentare { get; set; }


        public Participant(int id, string nume, string email, string telefon, string cnp, string pdfFilePath, int idPrezentare)
        {
            Id = id;
            Nume = nume;
            Email = email;
            Telefon = telefon;
            CNP = cnp;
            PdfFilePath = pdfFilePath;
            IdPrezentare = idPrezentare;
        }

        // Default constructor
        public Participant()
        {
        }


    }
}
