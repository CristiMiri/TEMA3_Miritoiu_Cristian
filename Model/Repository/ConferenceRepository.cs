using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{
    public class ConferenceRepository
    {
        private Repository repository;       

        public ConferenceRepository()
        {
            repository = Repository.Instance;
        }

        //Utility methods
        private static Conference RowToConference(DataRow row)
        {
            Conference conference = new Conference
            {
                Id = Convert.ToInt32(row["id"]),
                Title = row["title"].ToString(),
                Location = row["location"].ToString(),
                Date = Convert.ToDateTime(row["date"])
            };
            return conference;
        }


        //CRUD methods
        public bool CreateConference(Conference conference)
        {
            // Constructing SQL statement 
            string nonQuery = $"INSERT INTO conference (title, location, date) VALUES (" +
                              $"'{conference.Title}', " +
                              $"'{conference.Location}', " +
                              $"'{conference.Date:yyyy-MM-dd}')";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

        public List<Conference>? ReadConferences()
        {
            // Constructing SQL statement
            string query = "SELECT * FROM conference";
            DataTable conferenceTable = repository.ExecuteQuery(query);
            if (conferenceTable.Rows.Count == 0)
            {
                return null; // No conferences
            }
            // Convert DataTable to List<Conference>
            List<Conference> conferences = new List<Conference>();
            foreach (DataRow row in conferenceTable.Rows)
            {
                conferences.Add(RowToConference(row));
            }
            return conferences;
        }

        public Conference? ReadConferenceById(int id)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM conference WHERE id = {id}";
            DataTable conferenceTable = repository.ExecuteQuery(query);
            if (conferenceTable.Rows.Count == 0)
            {
                return null; // No conference with that id
            }
            // Convert DataTable to Conference
            return RowToConference(conferenceTable.Rows[0]);
        }

        public bool UpdateConference(Conference conference)
        {
            // Constructing SQL statement
            string nonQuery = $"UPDATE conference SET " +
                              $"title = '{conference.Title}', " +
                              $"location = '{conference.Location}', " +
                              $"date = '{conference.Date:yyyy-MM-dd}' " +
                              $"WHERE id = {conference.Id}";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool DeleteConference(int id)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM conference WHERE id = {id}";
            // Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }


    }
}
