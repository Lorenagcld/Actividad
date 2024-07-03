using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PF.Models
{
	public class Usuarios
	{
		[Key]
		public int idUsuario { get; set; }
		public string Clave { get; set; }
		public string Usuario { get; set; }
		public string Rol { get; set; }
		// Constantes para los roles
		public const string RolAdministrador = "Administrador";
		public const string RolCajero = "Cajero";
	}
}