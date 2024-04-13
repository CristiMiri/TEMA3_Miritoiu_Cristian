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
        /*Conferinta =  private int id;
                        private String titlu;
                        private String locatie;
                        private String data;
                        private List<Utilizator> participanti;
                        private List<Prezentare> prezentari;
                        */

        public ConferintaRepository()
        {
            repository = new Repository();
        }

        public DataTable ConferintaTable()
        {
            string query = "SELECT * FROM conferinte";
            DataTable conferinteTable = repository.ExecuteQuery(query);
            if (conferinteTable != null || conferinteTable.Rows.Count != 0)
            {
                return conferinteTable;
            }
            return null;
        }

        public Conferinta rowToConferinta(DataRow row)
        {

            Conferinta conferinta = new Conferinta();
            conferinta.Id = Convert.ToInt32(row["id"]);
            conferinta.Titlu = row["titlu"].ToString();
            conferinta.Locatie = row["locatie"].ToString();
            conferinta.Data = row["data"].ToString();
            return conferinta;
        }

        //CRUD
        public bool AddConferinta(Conferinta conferinta)
        {
            string query = "INSERT INTO conferinte( titlu, locatie, data) VALUES ('" +
                conferinta.Titlu + "', '" +
                conferinta.Locatie + "', '" +
                conferinta.Data + "')";
            return repository.ExecuteNonQuery(query);
        }

        public List<Conferinta> GetConferinte()
        {
            DataTable conferinteTable = ConferintaTable();
            if (conferinteTable == null)
            {
                return null;
            }
            List<Conferinta> conferinte = new List<Conferinta>();
            foreach (DataRow row in conferinteTable.Rows)
            {
                Conferinta conferinta = rowToConferinta(row);
                conferinte.Add(conferinta);
            }
            return conferinte;
        }

        public Conferinta GetConferintabyID(int id)
        {
            DataTable conferinteTable = ConferintaTable();
            if (conferinteTable == null)
            {
                return null;
            }
            foreach (DataRow row in conferinteTable.Rows)
            {
                Conferinta conferinta = rowToConferinta(row);
                if (conferinta.Id == id)
                {
                    return conferinta;
                }
            }
            return null;
        }

        public bool UpdateConferinta(Conferinta conferinta)
        {
            string query = "UPDATE conferinte SET titlu = '" + conferinta.Titlu +
                "', locatie = '" + conferinta.Locatie +
                "', data = '" + conferinta.Data +
                "' WHERE id = " + conferinta.Id; // Assuming there's an ID field to specify which row to update

            return repository.ExecuteNonQuery(query);
        }

        public bool DeleteConferinta(int id)
        {
            string query = "DELETE FROM conferinte WHERE id = " + id;
            return repository.ExecuteNonQuery(query);
        }

        //Filters


    }
}
