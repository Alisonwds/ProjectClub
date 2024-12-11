using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClub.Model
{
    public class Evento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int EspacoLocavelId { get; set; } // Relacionamento com EspacoLocavel
        public int AssociadoId { get; set; } // Relacionamento com Associado
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string NomeEvento { get; set; } = string.Empty;
        public decimal ValorLocacao { get; set; }
    }
}
