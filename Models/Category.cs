using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class Category
	{
		[Key]
		public int idCategoria { get; set; }
		public string Descripcion { get; set; }
		public ICollection<Productos> Producto { get; set; }
	}
}