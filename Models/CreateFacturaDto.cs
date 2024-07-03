using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class CreateFacturaDto
	{
		public int IdCliente { get; set; }
		public List<FacturaDetalleDto> Detalles { get; set; }
	}

	public class FacturaDetalleDto
	{
		public int IdProducto { get; set; }
		public double Cantidad { get; set; }
		public double Precio { get; set; }
		public double Descuento { get; set; }
	}
}