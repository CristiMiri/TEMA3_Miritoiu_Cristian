using DocumentFormat.OpenXml.Wordprocessing;
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
        private Participant_PrezentareRepository participant_PrezentareRepository;

        public ParticipantiRepository()
        {
            repository = new Repository();
            utilizatorRepository = new UtilizatorRepository();
            prezentareRepository = new PrezentareRepository();
            participant_PrezentareRepository = new Participant_PrezentareRepository(this, prezentareRepository);
        }

        //Utility methods
        private static Participant RowToParticipant(DataRow row)
        {
            return new Participant
            {
                Id = Convert.ToInt32(row["id"]),
                Nume = row["nume"].ToString(),
                Email = row["email"].ToString(),
                Telefon = row["telefon"].ToString(),
                CNP = row["cnp"].ToString(),
                PdfFilePath = row["pdf_file_path"].ToString(),
                PhotoFilePath = row["photo_file_path"].ToString(),
            };
        }

        //CRUD methods
        public bool CreateParticipant(Participant participant)
        {
            // Constructing SQL statement 
            string nonQuery = $"INSERT INTO participant (nume, email, telefon, cnp, pdf_file_path, photo_file_path) VALUES ('" +
                $"{participant.Nume}', '" +
                $"{participant.Email}', '" +
                $"{participant.Telefon}', '" +
                $"{participant.CNP}', '" +
                $"{participant.PdfFilePath}', '" +
                $"{participant.PhotoFilePath}')";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }
        public Participant? ReadParticipantById(int id)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM participant WHERE id = {id}";
            DataTable participantiTable = repository.ExecuteQuery(query);
            if (participantiTable.Rows.Count == 0)
            {
                return null;
            }
            return RowToParticipant(participantiTable.Rows[0]);
        }
        public List<Participant>? ReadParticipanti()
        {
            // Constructing SQL statement
            string query = "SELECT * FROM participant";
            DataTable participantiTable = repository.ExecuteQuery(query);
            if (participantiTable.Rows.Count == 0)
            {
                return null;
            }

            // Convert DataTable to List<Patricipant>
            List<Participant> participanti = new List<Participant>();
            foreach (DataRow row in participantiTable.Rows)
            {
                participanti.Add(RowToParticipant(row));
            }
            return participanti;
        }
        public bool UpdateParticipant(Participant participant)
        {
            // Constructing SQL statement
            string nonQuery = $"UPDATE participant SET " +
                $"nume = '{participant.Nume}', " +
                $"email = '{participant.Email}', " +
                $"telefon = '{participant.Telefon}', " +
                $"cnp = '{participant.CNP}', " +
                $"pdf_file_path = '{participant.PdfFilePath}', " +
                $"photo_file_path = '{participant.PhotoFilePath}' " +
                $"WHERE id = {participant.Id}";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }
        public bool DeleteParticipant(Participant participant)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM participant WHERE id = {participant.Id}";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }



        //Filter methods
        public List<Participant>? GetParticipantibySectiune(Sectiune sectiune)
        {            
            List<Prezentare> prezentari = prezentareRepository.ReadPrezentariBySectiune(sectiune);
            if (prezentari == null)
            {
                return null;
            }
            List<Participant> participanti = new List<Participant>();
            foreach(Prezentare p in prezentari)
            {
                List<Participant> participantiPrezentare = participant_PrezentareRepository.ReadRelationsbyRole<Participant>(p.Id, "VIZITATOR", true);
                if (participantiPrezentare != null)
                {
                    participanti.AddRange(participantiPrezentare);
                }
            }
            return participanti;

        }
                
    }

}
