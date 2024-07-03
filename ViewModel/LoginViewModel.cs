using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PF.ViewModel
{
	public class LoginViewModel
	{
		[Required]
		public string Usuario { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Clave { get; set; }
		[Display(Name = "Recordarme")]
		public bool RememberMe { get; set; }
	}
}