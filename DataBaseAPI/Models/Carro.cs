﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBaseAPI.Models
{
	public class Carro
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public decimal Valor { get; set; }

		public Carro() { }
	}
}