using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class Clientes
	{
		[Key]
		public int idCliente { get; set; }
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public string Cedula { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public virtual ICollection<Factura> Facturas { get; set; }
	}
}