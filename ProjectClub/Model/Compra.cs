using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClub.Model
{
    public class Compra
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int FornecedorId { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal TotalCompra { get; set; }
    }
}
