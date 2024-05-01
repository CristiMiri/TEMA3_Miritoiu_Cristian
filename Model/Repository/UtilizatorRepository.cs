using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{
    public class UtilizatorRepository

    {
        private Repository repository;        

        public UtilizatorRepository()
        {
            repository = new Repository();         
        }
        
        //Utility methods
        private static Utilizator RowToUtilizator(DataRow row)
        {
            Utilizator utilizator = new Utilizator();
            utilizator.Id = Convert.ToInt32(row["id"]);
            utilizator.Nume = row["nume"].ToString();
            utilizator.Email = row["email"].ToString();
            utilizator.Parola = row["parola"].ToString();
            utilizator.UserType = (UserType)Enum.Parse(typeof(UserType), row["user_type"].ToString());
            utilizator.Telefon = row["telefon"].ToString();
            return utilizator;
        }

        //CRUD methods
        public bool CreateUtilizator(Utilizator utilizator)
        {
            // Perform an SQL query to check if the user exists (avoids pulling the object)
            string existQuery = $"SELECT * FROM utilizator WHERE Email = '{utilizator.Email}'";
            DataTable dataTable = repository.ExecuteQuery(existQuery);
            if(dataTable.Rows.Count > 0)
            {
                return false; // User already exists
            }            
            // Constructing SQL statement 
            string nonQuery = $"INSERT INTO utilizator (Nume, Email, Parola, User_Type, Telefon) VALUES ('" +
                              $"{utilizator.Nume}', '" +
                              $"{utilizator.Email}', '" +
                              $"{utilizator.Parola}', '" +
                              $"{utilizator.UserType}', '" +
                              $"{utilizator.Telefon}')";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public Utilizator? ReadUtilizatorById(int id)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM utilizator WHERE id = {id}";
            DataTable utilizatoriTable = repository.ExecuteQuery(query);
            if (utilizatoriTable.Rows.Count == 0)
            {
                return null; // No utilizator with that id
            }
            // Convert DataTable to Utilizator
            return RowToUtilizator(utilizatoriTable.Rows[0]);

        }

        public List<Utilizator>? ReadUtilizatori()
        {
            // Constructing SQL statement
            string query = "SELECT * FROM utilizator";
            DataTable dataTable = repository.ExecuteQuery(query);
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            // Convert DataTable to List<Utilizator>
            List<Utilizator> utilizatori = new List<Utilizator>();
            foreach (DataRow row in dataTable.Rows)
            {
                utilizatori.Add(RowToUtilizator(row));
            }
            return utilizatori;
        }       

        public bool UpdateUtilizator(Utilizator utilizator)
        {
            // Constructing SQL statement
            string nonQuery = $"UPDATE utilizator SET " +
                              $"Nume = '{utilizator.Nume}', " +
                              $"Email = '{utilizator.Email}', " +
                              $"Parola = '{utilizator.Parola}', " +
                              $"User_Type = '{utilizator.UserType}', " +
                              $"Telefon = '{utilizator.Telefon}' " +
                              $"WHERE id = {utilizator.Id}";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool DeleteUtilizator(int id)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM utilizator WHERE id = {id}";
            return repository.ExecuteNonQuery(nonQuery);
        }


        //Filter methods        
        public List<Utilizator>? ReadUtilizatoribyUserType(UserType userType)
        {
            // Constructing SQL statement
            string type = userType.ToString();
            string query = $"SELECT * FROM utilizator WHERE User_Type = '{type}'";
            DataTable utilizatoriTable = repository.ExecuteQuery(query);
            if (utilizatoriTable.Rows.Count == 0)
            {
                return null; // No utilizator with that type
            }
            // Convert DataTable to List<Utilizator>
            List<Utilizator> utilizatori = new List<Utilizator>();
            foreach (DataRow row in utilizatoriTable.Rows)
            {
                utilizatori.Add(RowToUtilizator(row));
            }
            return utilizatori;
        }

        public Utilizator? ReadUtilizatorbyEmailandParola(string email, string parola)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM utilizator WHERE Email = '{email}' AND Parola = '{parola}'";
            DataTable utilizatoriTable = repository.ExecuteQuery(query);
            if(utilizatoriTable.Rows.Count == 0)
            {
                return null; // No utilizator with that email and password
            }
            // Convert DataTable to Utilizator
            return RowToUtilizator(utilizatoriTable.Rows[0]);            
        }        
    }
}
