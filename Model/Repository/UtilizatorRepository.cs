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
        private DataTable utilizatoriTable;
        /*User =    private int id;
                    private String nume;
                    private String email;
                    private String parola;
                    private UserType userType;
                    private String telefon; */
        public UtilizatorRepository()
        {
            repository = new Repository();
            utilizatoriTable = new DataTable();
        }

        public Repository Repository
        {
            get { return repository; }
            set { repository = value; }
        }

        public void UtilizatorTable()
        {
            string query = "SELECT * FROM utilizatori";
            utilizatoriTable = repository.ExecuteQuery(query);
            if (utilizatoriTable != null || utilizatoriTable.Rows.Count != 0)
            {
                this.utilizatoriTable = utilizatoriTable;
            }
        }

        public Utilizator rowToUtilizator(DataRow row)
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


        public DataTable UtilizatoriTable
        {
            get { return utilizatoriTable; }
            set { utilizatoriTable = value; }
        }

        //CRUD
        public bool addUtilizator(Utilizator utilizator)
        {
            Utilizator utilizatorExist = GetUtilizatorbyEmail(utilizator.Email);
            if (utilizatorExist != null)
            {
                return false;
            }
            string nonQuery = "INSERT INTO utilizatori (Nume, Email, Parola, User_Type, Telefon) VALUES ('" +
                utilizator.Nume + "', '" +
                utilizator.Email + "', '" +
                utilizator.Parola + "', '" +
                utilizator.UserType + "', '" +
                utilizator.Telefon + "')";

            return repository.ExecuteNonQuery(nonQuery);

        }

        public List<Utilizator> GetUtilizatori()
        {
            UtilizatorTable();
            if (utilizatoriTable == null)
            {
                return null;
            }
            List<Utilizator> utilizatori = new List<Utilizator>();
            foreach (DataRow row in utilizatoriTable.Rows)
            {
                utilizatori.Add(rowToUtilizator(row));
            }
            return utilizatori;
        }

        public Utilizator GetUtilizatorById(int id)
        {
            UtilizatorTable();
            if (utilizatoriTable == null)
            {
                return null;
            }
            foreach (DataRow row in utilizatoriTable.Rows)
            {
                if (Convert.ToInt32(row["id"]) == id)
                {
                    return rowToUtilizator(row);
                }
            }
            return null;
        }

        public bool updateUtilizator(Utilizator utilizator)
        {
            string nonQuery = "update utilizatori set " +
                "nume = '" + utilizator.Nume + "', " +
                "email = '" + utilizator.Email + "', " +
                "parola = '" + utilizator.Parola + "', " +
                "user_type = '" + utilizator.UserType + "', " +
                "telefon = '" + utilizator.Telefon + "' " +
                "where id = " + utilizator.Id;
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool deleteUtilizator(int id)
        {
            string nonQuery = "delete from utilizatori where id = " + id;
            return repository.ExecuteNonQuery(nonQuery);
        }

        //Filters
        public Utilizator GetUtilizatorbyNume(string nume)
        {
            UtilizatorTable();
            if (utilizatoriTable == null)
            {
                return null;
            }
            foreach (DataRow row in utilizatoriTable.Rows)
            {
                if (row["nume"].ToString() == nume)
                {
                    return rowToUtilizator(row);
                }
            }
            return null;
        }
        public List<Utilizator> GetUtilizatorsbyUserType(UserType userType)
        {
            UtilizatorTable();
            if (utilizatoriTable == null)
            {
                return null;
            }
            List<Utilizator> utilizatori = new List<Utilizator>();
            foreach (DataRow row in utilizatoriTable.Rows)
            {
                if ((UserType)Enum.Parse(typeof(UserType), row["usertype"].ToString()) == userType)
                {
                    utilizatori.Add(rowToUtilizator(row));
                }
            }
            return utilizatori;
        }
        public Utilizator GetUtilizatorbyEmailandParola(string email, string parola)
        {
            UtilizatorTable();
            if (utilizatoriTable == null)
            {
                return null;
            }
            foreach (DataRow row in utilizatoriTable.Rows)
            {
                if (row["email"].ToString() == email && row["parola"].ToString() == parola)
                {
                    return rowToUtilizator(row);
                }
            }
            return null;
        }
        public Utilizator GetUtilizatorbyEmail(string email)
        {
            UtilizatorTable();
            if (utilizatoriTable == null)
            {
                return null;
            }
            foreach (DataRow row in utilizatoriTable.Rows)
            {
                if (row["email"].ToString() == email)
                {
                    return rowToUtilizator(row);
                }
            }
            return null;
        }
    }
}
