using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{    
    public class PrezentareRepository
    {
        private Repository repository;
        private DataTable prezentariTable;

        public PrezentareRepository()
        {
            repository = new Repository();
        }

        private Prezentare RowToPrezentare(DataRow row)
        {
            return new Prezentare
            {
                Id = Convert.ToInt32(row["id"]),
                Titlu = row["titlu"].ToString(),
                IdAutor = Convert.ToInt32(row["id_autor"]),
                Descriere = row["descriere"].ToString(),
                Data = Convert.ToDateTime(row["data"]),
                Ora = TimeSpan.Parse(row["ora"].ToString()),
                Sectiune = (Sectiune)Enum.Parse(typeof(Sectiune), row["sectiune"].ToString()),
                IdConferinta = Convert.ToInt32(row["id_conferinta"])
            };
        }

        public List<Prezentare> GetPrezentari()
        {
            string query = "SELECT * FROM prezentari";
            DataTable table = repository.ExecuteQuery(query);
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in table.Rows)
            {
                prezentari.Add(RowToPrezentare(row));
            }
            return prezentari;
        }

        public Prezentare GetPrezentareById(int id)
        {
            string query = $"SELECT * FROM prezentari WHERE id = {id}";
            DataTable table = repository.ExecuteQuery(query);
            if (table.Rows.Count > 0)
            {
                return RowToPrezentare(table.Rows[0]);
            }
            return null;
        }

        public bool AddPrezentare(Prezentare prezentare)
        {
            string query = $"INSERT INTO prezentari (titlu, id_autor, descriere, data, ora, sectiune, id_conferinta) VALUES ('{prezentare.Titlu}', {prezentare.IdAutor}, '{prezentare.Descriere}', '{prezentare.Data:yyyy-MM-dd}', '{prezentare.Ora}', '{prezentare.Sectiune}', {prezentare.IdConferinta})";
            return repository.ExecuteNonQuery(query);
        }

        public bool UpdatePrezentare(Prezentare prezentare)
        {
            string query = $"UPDATE prezentari SET titlu = '{prezentare.Titlu}', id_autor = {prezentare.IdAutor}, descriere = '{prezentare.Descriere}', data = '{prezentare.Data:yyyy-MM-dd}', ora = '{prezentare.Ora}', sectiune = '{prezentare.Sectiune}', id_conferinta = {prezentare.IdConferinta} WHERE id = {prezentare.Id}";
            return repository.ExecuteNonQuery(query);
        }

        public bool DeletePrezentare(int id)
        {
            string query = $"DELETE FROM prezentari WHERE id = {id}";
            return repository.ExecuteNonQuery(query);
        }

        public void PrezentareTable()
        {
            string query = "SELECT * FROM prezentari";
            prezentariTable = repository.ExecuteQuery(query);
            if (prezentariTable != null || prezentariTable.Rows.Count != 0)
            {
                this.prezentariTable = prezentariTable;
            }
        }
        //Filters

        public List<Prezentare> getPrezentariBySectiune(Sectiune sectiune)
        {
            PrezentareTable();
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in prezentariTable.Rows)
            {
                if (row["sectiune"].ToString().Equals(sectiune.ToString()))
                {
                    prezentari.Add(RowToPrezentare(row));
                }
            }
            return prezentari;
        }

        public List<Prezentare> GetPrezentarebySectiune(Sectiune sectiune)
        {
            PrezentareTable();
            List<Prezentare> prezentari = new List<Prezentare>();
            foreach (DataRow row in prezentariTable.Rows)
            {
                if (row["sectiune"].ToString().Equals(sectiune.ToString()))
                {
                    prezentari.Add(RowToPrezentare(row));
                }
            }
            return prezentari;
        }

        public Prezentare GetPrezentarebyTitlu(string titlu)
        {
            PrezentareTable();
            foreach (DataRow row in prezentariTable.Rows)
            {
                if (row["titlu"].ToString().Equals(titlu))
                {
                    return RowToPrezentare(row);
                }
            }
            return null;
        }
    }

}
