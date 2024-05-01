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
        private ParticipantiRepository participantRepository;
        private PrezentareRepository prezentareRepository;

        public Participant_PrezentareRepository(ParticipantiRepository participantiRepository, PrezentareRepository prezentareRepository)
        {
            repository = new Repository();
            participantRepository = participantiRepository;
            prezentareRepository = prezentareRepository;
        }
        
        //Create
        public bool CreateRelation(Participant participant, Prezentare prezentare)
        {
            //Constructing SQL statement
            string nonQuery = $"INSERT INTO prezentare_participant (id_prezentare, id_participant) VALUES (" +
                $"{prezentare.Id}, " +
                $"{participant.Id})";
            //Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }
        //Read
        public List<Participant> 
        //Delete
        private bool DeleteRelation(Participant participant, Prezentare prezentare)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM prezentare_participant WHERE id_participant = {participant.Id} AND id_prezentare = {prezentare.Id} ;";
            return repository.ExecuteNonQuery(nonQuery);
        }
    }
}
