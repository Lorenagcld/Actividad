using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class facturaDetalle
	{
			[Key]
			public int idFacturaDetalle { get; set; }
			public int idFactura { get; set; }
			public virtual Factura Facturas { get; set; }
			public int IdProducto { get; set; }
			public virtual Productos Producto { get; set; }
			public double Cantidad { get; set; }
			public double Precio { get; set; }
			public double Descuento { get; set; }
			public double Total { get; set; }
		
	}
}