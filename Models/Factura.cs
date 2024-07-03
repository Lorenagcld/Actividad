using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class Factura
	{
			[Key]
			public int idFactura { get; set; }
			public DateTime Fecha { get; set; }
			public int idCliente { get; set; }
			public virtual Clientes Cliente { get; set; }
			public double SubTotal { get; set; }
			public double Iva { get; set; }
			public float Total { get; set; }
			public virtual ICollection<facturaDetalle> FacturaDetalles { get; set; }
		
	}
}