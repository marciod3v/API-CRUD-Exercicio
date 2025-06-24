using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataBaseAPI.Models
{

    public class Funcionario
    {
        public int Codigo { get; set; }
        [Required]
        public int CodigoDepartamento { get; set; }
        [Required]
        public string PrimeiroNome { get; set; }
        [Required]
        public string SegundoNome { get; set; }
        [Required]
        public string UltimoNome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string RG { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Fone { get; set; }
        [Required]
        public string Funcao { get; set; }
        [Required]
        public decimal Salario { get; set; }

        public Funcionario()
        {
        }

    }
}