using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{
    public class ParticipantRepository
    {
        private Repository repository;
        private UserRepository userRepository;
        private PresentationRepository presentationRepository;
        private Participant_PrezentareRepository participant_PrezentareRepository;

        public ParticipantRepository()
        {
            repository = Repository.Instance;
            userRepository = new UserRepository();
            presentationRepository= new PresentationRepository();
            participant_PrezentareRepository = new Participant_PrezentareRepository(this, presentationRepository);
        }

        //Utility methods
        private static Participant RowToParticipant(DataRow row)
        {
            return new Participant
            {
                Id = Convert.ToInt32(row["id"]),
                Name = row["name"].ToString(),
                Email = row["email"].ToString(),
                Phone = row["phone"].ToString(),
                CNP = row["cnp"].ToString(),
                PdfFilePath = row["pdf_file_path"].ToString(),
                PhotoFilePath = row["photo_file_path"].ToString(),
            };
        }


        //CRUD methods
        public bool CreateParticipant(Participant participant)
        {
            // Constructing SQL statement 
            string nonQuery = $"INSERT INTO participant (name, email, phone, cnp, pdf_file_path, photo_file_path) VALUES ('" +
                $"{participant.Name}', '" +
                $"{participant.Email}', '" +
                $"{participant.Phone}', '" +
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

        public List<Participant>? ReadParticipants()
        {
            // Constructing SQL statement
            string query = "SELECT * FROM participant";
            DataTable participantiTable = repository.ExecuteQuery(query);
            if (participantiTable.Rows.Count == 0)
            {
                return null;
            }

            // Convert DataTable to List<Participant>
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
                $"name = '{participant.Name}', " +
                $"email = '{participant.Email}', " +
                $"phone = '{participant.Phone}', " +
                $"cnp = '{participant.CNP}', " +
                $"pdf_file_path = '{participant.PdfFilePath}', " +
                $"photo_file_path = '{participant.PhotoFilePath}' " +
                $"WHERE id = {participant.Id}";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool DeleteParticipant(int id)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM participant WHERE id = {id}";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool UpsertParticipant(Participant participant)
        {
            if (ReadParticipantById(participant.Id) == null)
            {
                return CreateParticipant(participant);
            }
            return UpdateParticipant(participant);
        }

        //Filter methods
        public List<Participant>? GetParticipantsBySection(Section section)
        {
            List<Presentation> presentations = presentationRepository.ReadPresentationsBySection(section);
            if (presentations == null)
            {
                return null;
            }
            List<Participant> participants = new List<Participant>();
            foreach (Presentation presentation in presentations)
            {
                List<Participant> presentationParticipants = participant_PrezentareRepository.ReadParticipantsByPresentation(presentation);
                if (presentationParticipants != null)
                {
                    participants.AddRange(presentationParticipants);
                }
            }
            return participants;
        }

        public int GetLastParticipantId()
        {
            // Constructing SQL statement
            string query = "SELECT MAX(id) FROM participant";
            DataTable participantiTable = repository.ExecuteQuery(query);
            if (participantiTable.Rows.Count == 0)
            {
                return 0;
            }
            return Convert.ToInt32(participantiTable.Rows[0][0]);
        }
    }

}
