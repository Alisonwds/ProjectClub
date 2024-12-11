using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClub.Model
{
    public class Agenda
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int AssociadoId { get; set; }
        public int EventoId { get; set; }
        public DateTime DataHorario { get; set; }
        public string Descricao { get; set; }
    }
}
