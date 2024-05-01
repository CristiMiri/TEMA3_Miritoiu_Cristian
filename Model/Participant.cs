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
        public string PhotoFilePath { get; set; }          

        // Full constructor initializing all properties
        public Participant(int id, string nume, string email, string telefon, string cnp, string pdfFilePath, string photoFilePath)
        {
            Id = id;
            Nume = nume;
            Email = email;
            Telefon = telefon;
            CNP = cnp;
            PdfFilePath = pdfFilePath;
            PhotoFilePath = photoFilePath;             
        }

        // Default constructor for initializing with default values
        public Participant()
        {
            Id = 0;
            Nume = "";
            Email = "";
            Telefon = "";
            CNP = "";
            PdfFilePath = "";
            PhotoFilePath = "";              
        }

        public Participant(Participant participant)
        {
            Id = participant.Id;
            Nume = participant.Nume;
            Email = participant.Email;
            Telefon = participant.Telefon;
            CNP = participant.CNP;
            PdfFilePath = participant.PdfFilePath;
            PhotoFilePath = participant.PhotoFilePath;             
        }

    }
}
