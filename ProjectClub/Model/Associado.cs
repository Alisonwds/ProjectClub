﻿ using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClub.Model
{
    public class Associado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomeTitular { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Facebook { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string RegistroGeral { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string TipoDeAssociacao { get; set; } = string.Empty;
    }
}
