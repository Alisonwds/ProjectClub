using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClub.Model
{
    public class EspacoLocavel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomeEspaco { get; set; } = string.Empty;
        public string Tamanho { get; set; } = string.Empty;
        public int CapacidadePessoas { get; set; }
        public DateTime DataConstrucao { get; set; }
        public bool Local { get; set; }
    }
}
