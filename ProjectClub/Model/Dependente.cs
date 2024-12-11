using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClub.Model
{
    public class Dependente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string RegistroGeral { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string TipoDeVinculo { get; set; } = string.Empty;
        public int SocioTitularId { get; set; } // Relacionamento com Associado
    }
}
