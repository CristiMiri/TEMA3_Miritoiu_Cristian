using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{
    internal class Participant_PrezentareRepository
    {
        private Repository repository;
        private ParticipantRepository participantRepository;
        private PresentationRepository prezentareRepository;

        public Participant_PrezentareRepository(ParticipantRepository participantRepository, PresentationRepository prezentareRepository)
        {
            this.repository = Repository.Instance;
            this.participantRepository = participantRepository;
            this.prezentareRepository = prezentareRepository;
        }

        // Create
        public bool CreateRelation(Participant participant, Presentation prezentare)
        {
            // Constructing SQL statement
            string nonQuery = $"INSERT INTO presentation_Participant (id_presentation, id_participant) VALUES (" +
                              $"{prezentare.Id}, " +
                              $"{participant.Id})";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

        // Read Participants by Presentation
        public List<Participant> ReadParticipantsByPresentation(Presentation prezentare)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM presentation_Participant WHERE id_presentation = {prezentare.Id}";
            DataTable dataTable = repository.ExecuteQuery(query);
            List<Participant> participants = new List<Participant>();

            // Convert DataTable to List<Participant>
            foreach (DataRow row in dataTable.Rows)
            {
                int idParticipant = Convert.ToInt32(row["id_participant"]);
                Participant participant = participantRepository.ReadParticipantById(idParticipant);
                participants.Add(participant);
            }
            return participants;
        }

        // Read Presentations by Participant
        public List<Presentation> ReadPresentationsByParticipant(Participant participant)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM presentation_Participant WHERE id_participant = {participant.Id}";
            DataTable dataTable = repository.ExecuteQuery(query);
            List<Presentation> presentations = new List<Presentation>();

            // Convert DataTable to List<Presentation>
            foreach (DataRow row in dataTable.Rows)
            {
                int idPresentation = Convert.ToInt32(row["id_presentation"]);
                Presentation presentation = prezentareRepository.ReadPresentationById(idPresentation);
                presentations.Add(presentation);
            }
            return presentations;
        }

        // Delete
        public bool DeleteRelation(Participant participant, Presentation prezentare)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM presentation_Participant WHERE id_participant = {participant.Id} AND id_presentation = {prezentare.Id}";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool CreateParticipantPresentation(int idParticipant, object idPresentation)
        {
            string nonQuery = $"INSERT INTO presentation_Participant (id_participant, id_presentation) VALUES ({idParticipant}, {idPresentation})";
            return repository.ExecuteNonQuery(nonQuery);
        }
    }

}
