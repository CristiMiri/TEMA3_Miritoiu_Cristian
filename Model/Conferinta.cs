using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model
{
    public class Conferinta
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Locatie { get; set; }
        public string Data { get; set; }

        public Conferinta(int id, string titlu, string locatie, string data)
        {
            this.Id = id;
            this.Titlu = titlu;
            this.Locatie = locatie;
            this.Data = data;
        }

        public Conferinta(Conferinta conferinta)
        {
            this.Id = conferinta.Id;
            this.Titlu = conferinta.Titlu;
            this.Locatie = conferinta.Locatie;
            this.Data = conferinta.Data;

        }

        public Conferinta()
        {
            this.Id = 0;
            this.Titlu = "";
            this.Locatie = "";
            this.Data = "";
        }
    }
}
