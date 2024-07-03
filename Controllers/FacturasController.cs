using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PF.Data;
using PF.Models;
using Rotativa;
namespace PF.Controllers
{
    public class FacturasController : Controller
    {
		private AppDbContext db = new AppDbContext();

		// GET: Facturas/Create
		public ActionResult Create()
		{
			ViewBag.Clientes = new SelectList(db.cliente, "idCliente", "Nombres");
			ViewBag.Productos = new SelectList(db.productos, "idProducto", "Name");
			return View();
		}

		// POST: Facturas/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateFacturaDto facturaDto)
		{
			if (ModelState.IsValid)
			{
				var cliente = db.cliente.Find(facturaDto.IdCliente);
				if (cliente == null)
				{
					ModelState.AddModelError("", "Cliente no encontrado.");
					ViewBag.Clientes = new SelectList(db.cliente, "idCliente", "Nombres", facturaDto.IdCliente);
					ViewBag.Productos = new SelectList(db.productos, "idProducto", "Name");
					return View(facturaDto);
				}

				var subtotal = facturaDto.Detalles.Sum(d => d.Cantidad * d.Precio - d.Descuento);
				var iva = subtotal * 0.15;
				var total = subtotal + iva;

				Factura factura = new Factura
				{
					Fecha = DateTime.Now,
					idCliente = facturaDto.IdCliente,
					Cliente = cliente,
					SubTotal = subtotal,
					Iva = iva,
					Total = (float)total,
					FacturaDetalles = new List<facturaDetalle>()
				};

				foreach (var detalleDto in facturaDto.Detalles)
				{
					var producto = db.productos.Find(detalleDto.IdProducto);
					if (producto == null)
					{
						ModelState.AddModelError("", "Producto no encontrado.");
						ViewBag.Clientes = new SelectList(db.cliente, "idCliente", "Nombres", facturaDto.IdCliente);
						ViewBag.Productos = new SelectList(db.productos, "idProducto", "Name");
						return View(facturaDto);
					}

					factura.FacturaDetalles.Add(new facturaDetalle
					{
						IdProducto = detalleDto.IdProducto,
						Producto = producto,
						Cantidad = detalleDto.Cantidad,
						Precio = detalleDto.Precio,
						Descuento = detalleDto.Descuento,
						Total = (detalleDto.Cantidad * detalleDto.Precio) - detalleDto.Descuento
					});
				}

				db.Facturas.Add(factura);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.Clientes = new SelectList(db.cliente, "idCliente", "Nombres", facturaDto.IdCliente);
			ViewBag.Productos = new SelectList(db.productos, "idProducto", "Name");
			return View(facturaDto);
		}

		// GET: Facturas
		public ActionResult Index()
		{
			var facturas = db.Facturas.Include("Cliente").ToList(); // Obtener todas las facturas incluyendo el cliente asociado
			return View(facturas);
		}

		// Método Dispose para liberar recursos
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		[HttpGet]
		public JsonResult BuscarClientePorId(int idCliente)
		{
			var cliente = db.cliente.Find(idCliente);
			if (cliente == null)
			{
				return Json(new { success = false, message = "Cliente no encontrado." }, JsonRequestBehavior.AllowGet);
			}
			return Json(new { success = true, cliente }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult BuscarProductoPorId(int id)
		{
			var producto = db.productos.Find(id);
			if (producto == null)
			{
				return Json(new { success = false, message = "Producto no encontrado." }, JsonRequestBehavior.AllowGet);
			}
			return Json(new { success = true, producto }, JsonRequestBehavior.AllowGet);
		}
		// GET: Facturas/Details/{id}
		public ActionResult Details(int id)
		{
			var factura = db.Facturas.Include(f => f.Cliente).Include(f => f.FacturaDetalles).FirstOrDefault(f => f.idFactura == id);
			if (factura == null)
			{
				return HttpNotFound();
			}
			return View(factura);
		}
		public ActionResult GenerarPdf(int id)
		{
			var factura = db.Facturas.Include(f => f.Cliente).Include(f => f.FacturaDetalles).FirstOrDefault(f => f.idFactura == id);
			if (factura == null)
			{
				return HttpNotFound();
			}

			return new ViewAsPdf("DetallesPdf", factura)
			{
				FileName = "Factura.pdf"
			};
		}
	}
}