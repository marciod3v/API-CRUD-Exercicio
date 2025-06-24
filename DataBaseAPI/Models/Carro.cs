using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataBaseAPI.Models
{
	public class Carro
	{
		public int Id { get; set; }

		[Required]
		public string Nome { get; set; }
		[Required]
		public decimal Valor { get; set; }

		public Carro() { }
	}
}