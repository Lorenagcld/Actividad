using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class FacturaDetalleDTO
	{
		public int idCliente { get; set; }
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public string Cedula { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public int idProducto { get; set; }
		public string ProductoNombre { get; set; }
		public string CodigoBarras { get; set; }
		public int idCategoria { get; set; }
		public int idFacturaDetalle { get; set; }
		public double Cantidad { get; set; }
		public double Precio { get; set; }
		public double Descuento { get; set; }
		public double DetalleTotal { get; set; }
		public int idFactura { get; set; }
		public DateTime Fecha { get; set; }
		public double SubTotal { get; set; }
		public double Iva { get; set; }
		public float FacturaTotal { get; set; }
	}
}