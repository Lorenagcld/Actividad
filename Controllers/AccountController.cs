using PF.Data;
using PF.Models;
using PF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PF.Controllers
{
    public class AccountController : Controller
    {
		private readonly AppDbContext _appDbContext;
		public AccountController()
		{
			_appDbContext = new AppDbContext();
		}
		public ActionResult Index()
		{
			if (User.IsInRole(Usuarios.RolAdministrador))
			{
				ViewBag.TipoUsuario = "Administrador";
			}
			else if (User.IsInRole(Usuarios.RolCajero))
			{
				ViewBag.TipoUsuario = "Cajero";
			}
			else
			{
				ViewBag.TipoUsuario = "UsuarioNormal";
			}

			return View();
		}


		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Verifica las credenciales del usuario
				var usuario = _appDbContext.usuario.SingleOrDefault(u => u.Usuario == model.Usuario && u.Clave == model.Clave);
				if (usuario != null)
				{
					// Si las credenciales son válidas, establece la cookie de autenticación
					FormsAuthentication.SetAuthCookie(usuario.Usuario, model.RememberMe);

					// Asigna el rol a la sesión
					Session["Rol"] = usuario.Rol;

					// Redirige según el rol del usuario
					if (usuario.Rol == Usuarios.RolAdministrador)
					{
						return RedirectToAction("Index", "Home");
					}
					else if (usuario.Rol == Usuarios.RolCajero)
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						// En caso de que haya un rol no reconocido
						ModelState.AddModelError("", "Rol de usuario no reconocido.");
					}
				}
				else
				{
					ModelState.AddModelError("", "Credenciales inválidas.");
				}
			}
			return View(model);
		}

		// GET: Cuenta/Logout
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Clear();
			return RedirectToAction("Login");
		}
	}
}