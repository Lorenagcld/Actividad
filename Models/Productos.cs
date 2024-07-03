using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class Productos
	{
		[Key]
		public int idProducto { get; set; }
		public string Name { get; set; }
		public string CodigoBarras { get; set; }
		public int idCategoria { get; set; }

		public Category Categoria { get; set; }
		public virtual ICollection<facturaDetalle> FacturaDetalles { get; set; }
	}
}