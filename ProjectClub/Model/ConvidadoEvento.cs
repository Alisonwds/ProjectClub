using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClub.Model
{
    public class ConvidadoEvento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int ConvidadoId { get; set; }
    }
}
