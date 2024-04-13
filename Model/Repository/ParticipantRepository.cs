using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{
    public class ParticipantiRepository
    {
        private Repository repository;
        private UtilizatorRepository utilizatorRepository;
        private PrezentareRepository prezentareRepository;

        public ParticipantiRepository()
        {
            repository = new Repository();
            utilizatorRepository = new UtilizatorRepository();
            prezentareRepository = new PrezentareRepository();
        }

        private Participant rowToParticipant(DataRow row)
        {
            return new Participant
            {
                Id = Convert.ToInt32(row["id"]),
                Nume = row["nume"].ToString(),
                Email = row["email"].ToString(),
                Telefon = row["telefon"].ToString(),
                CNP = row["cnp"].ToString(),
                PdfFilePath = row["pdf_file_path"].ToString(),
                IdPrezentare = Convert.ToInt32(row["id_prezentare"])
            };
        }
        public DataTable ParticipantiTable()
        {
            string query = "SELECT * FROM participanti";
            DataTable participantiTable = repository.ExecuteQuery(query);
            return participantiTable;
        }

        public List<Participant> GetParticipanti()
        {
            DataTable participantiTable = ParticipantiTable();
            List<Participant> participanti = new List<Participant>();
            if (participantiTable != null && participantiTable.Rows.Count > 0)
            {
                foreach (DataRow row in participantiTable.Rows)
                {
                    participanti.Add(rowToParticipant(row));
                }
            }
            return participanti;
        }

        public bool AddParticipant(Participant participant)
        {
            string nonQuery = $"INSERT INTO participanti (nume, email, telefon, cnp, pdf_file_path, id_prezentare) VALUES ('{participant.Nume}', '{participant.Email}', '{participant.Telefon}', '{participant.CNP}', '{participant.PdfFilePath}', {participant.IdPrezentare})";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool DeleteParticipant(Participant participant)
        {
            string nonQuery = $"DELETE FROM participanti WHERE id = {participant.Id}";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool UpdateParticipant(Participant participant)
        {
            string nonQuery = $"UPDATE participanti SET nume = '{participant.Nume}', email = '{participant.Email}', telefon = '{participant.Telefon}', cnp = '{participant.CNP}', pdf_file_path = '{participant.PdfFilePath}', id_prezentare = {participant.IdPrezentare} WHERE id = {participant.Id}";
            return repository.ExecuteNonQuery(nonQuery);
        }


        public List<Participant> GetParticipantibySectiune(Sectiune sectiune)
        {
            List<Prezentare> prezentari = prezentareRepository.GetPrezentarebySectiune(sectiune);
            List<Participant> participanti = new List<Participant>();
            foreach (Prezentare prezentare in prezentari)
            {
                List<Participant> participantiPrezentare = GetParticipantibyPrezentare(prezentare);
                if (participantiPrezentare != null)
                {
                    participanti.AddRange(participantiPrezentare);
                }
            }
            return participanti;

        }


        public List<Participant> GetParticipantibyPrezentare(Prezentare prezentare)
        {
            string query = "SELECT * FROM participanti WHERE id_prezentare = " + prezentare.Id;
            DataTable participantiTable = repository.ExecuteQuery(query);
            if (participantiTable != null || participantiTable.Rows.Count > 0)
            {
                List<Participant> participanti = new List<Participant>();
                foreach (DataRow row in participantiTable.Rows)
                {
                    participanti.Add(rowToParticipant(row));
                }
                return participanti;
            }
            return null;
        }
    }

}
