using System.Data;

namespace PS_TEMA3.Model.Repository
{
    public class PrezentareRepository
    {
        private Repository repository;        

        public PrezentareRepository()
        {
            repository = new Repository();
        }


        //Utility methods
        private Prezentare RowToPrezentare(DataRow row)
        {
            return new Prezentare
            {
                Id = Convert.ToInt32(row["id"]),
                Titlu = row["titlu"].ToString(),
                Descriere = row["descriere"].ToString(),
                Data = Convert.ToDateTime(row["data"]),  
                Ora = TimeSpan.Parse(row["ora"].ToString()),  
                Sectiune = (Sectiune)Enum.Parse(typeof(Sectiune), row["sectiune"].ToString()),
                IdConferinta = Convert.ToInt32(row["id_conferinta"])
            };
        }        


        //CRUD methods
        public bool CreatePrezentare(Prezentare prezentare)
        {
            // Safely format the date and time to avoid SQL injection, but this is minimal and does not provide complete protection.
            string safeDate = prezentare.Data.ToString("yyyy-MM-dd");
            string safeTime = prezentare.Ora.ToString();
            
            string titlu = prezentare.Titlu.Replace("'", "''");
            string descriere = prezentare.Descriere.Replace("'", "''");

            string query = $"INSERT INTO prezentare (titlu, descriere, data, ora, sectiune, id_conferinta) " +
                $"VALUES ('{titlu}', " +
                $"'{descriere}', " +
                $"'{safeDate}', " +
                $"'{safeTime}', " +
                $"'{prezentare.Sectiune}', " +
                $"{prezentare.IdConferinta})";
            return repository.ExecuteNonQuery(query);
        }

        public Prezentare? ReadPrezentareById(int id)
        {
            string query = $"SELECT * FROM prezentare WHERE id = {id}";
            DataTable table = repository.ExecuteQuery(query);
            if (table.Rows.Count > 0)
            {
                return RowToPrezentare(table.Rows[0]);
            }
            return null;
        }

        public List<Prezentare>? ReadPrezentari()
        {
            string query = "SELECT * FROM prezentare";
            DataTable table = repository.ExecuteQuery(query);
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in table.Rows)
            {
                prezentari.Add(RowToPrezentare(row));
            }
            return prezentari;
        }      

        public bool UpdatePrezentare(Prezentare prezentare)
        {            
            string titluEscaped = prezentare.Titlu.Replace("'", "''");
            string descriereEscaped = prezentare.Descriere.Replace("'", "''");

            // Format dates and enums properly.
            string formattedDate = prezentare.Data.ToString("yyyy-MM-dd");
            string formattedTime = prezentare.Ora.ToString();
            string sectiuneAsString = prezentare.Sectiune.ToString(); 

            string query = $@"
                UPDATE prezentare
                SET
                    titlu = '{titluEscaped}',
                    descriere = '{descriereEscaped}',
                    data = '{formattedDate}',
                    ora = '{formattedTime}',
                    sectiune = '{sectiuneAsString}',
                    id_conferinta = {prezentare.IdConferinta}
                WHERE id = {prezentare.Id}";
            return repository.ExecuteNonQuery(query);
        }

        public bool DeletePrezentare(int id)
        {
            string query = $"DELETE FROM prezentare WHERE id = {id}";
            return repository.ExecuteNonQuery(query);
        }


        //Filter methods
        public List<Prezentare> ReadPrezentariBySectiune(Sectiune sectiune)
        {
            string query = $"SELECT * FROM prezentare WHERE sectiune = '{sectiune}'";
            DataTable table = repository.ExecuteQuery(query);
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in table.Rows)
            {
                prezentari.Add(RowToPrezentare(row));
            }
            return prezentari;
        }
        
    }

}
