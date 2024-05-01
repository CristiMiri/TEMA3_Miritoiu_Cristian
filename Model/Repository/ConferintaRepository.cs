using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{
    public class ConferintaRepository
    {
        private Repository repository;       

        public ConferintaRepository()
        {
            repository = new Repository();
        }

        //Utility methods
        private static Conferinta RowToConferinta(DataRow row)
        {

            Conferinta conferinta = new Conferinta
            {
                Id = Convert.ToInt32(row["id"]),
                Titlu = row["titlu"].ToString(),
                Locatie = row["locatie"].ToString(),
                Data = Convert.ToDateTime(row["data"])
            };
            return conferinta;
        }

        //CRUD methods
        public bool CreateConferinta(Conferinta conferinta)
        {
            // Constructing SQL statement 
            string nonQuery = $"INSERT INTO conferinta (titlu, locatie, data) VALUES ('" +
                              $"{conferinta.Titlu}', '" +
                              $"{conferinta.Locatie}', '" +
                              $"{conferinta.Data}')";
            //Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

        public List<Conferinta>? ReadConferinte()
        {
            // Constructing SQL statement
            string query = "SELECT * FROM conferinta";
            DataTable conferinteTable = repository.ExecuteQuery(query);
            if (conferinteTable.Rows.Count == 0)
            {
                return null; // No conferinte
            }
            // Convert DataTable to List<Conferinta>
            List<Conferinta> conferinte = new List<Conferinta>();
            foreach (DataRow row in conferinteTable.Rows)
            {
                conferinte.Add(RowToConferinta(row));
            }
            return conferinte;
        }

        public Conferinta? ReadConferintabyID(int id)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM conferinta WHERE id = {id}";
            DataTable conferinteTable = repository.ExecuteQuery(query);
            if (conferinteTable.Rows.Count == 0)
            {
                return null; // No conferinte with that id
            }
            // Convert DataTable to Conferinta
            return RowToConferinta(conferinteTable.Rows[0]);
        }

        public bool UpdateConferinta(Conferinta conferinta)
        {
            // Constructing SQL statement
            string nonQuery = $"UPDATE conferinta SET " +
                              $"titlu = '{conferinta.Titlu}', " +
                              $"locatie = '{conferinta.Locatie}', " +
                              $"data = '{conferinta.Data}' " +
                              $"WHERE id = {conferinta.Id}";
            //Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool DeleteConferinta(int id)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM conferinta WHERE id = {id}";
            //Execute the query
            return repository.ExecuteNonQuery(nonQuery);
        }

    }
}
