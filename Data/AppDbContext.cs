using PF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Xml.Linq;


namespace PF.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Productos> productos { get; set; }
		public DbSet<Category> TiposProducto { get; set; }
		public DbSet<Usuarios> usuario { get; set; }
		public DbSet<Clientes> cliente { get; set; }
		public DbSet<Factura> Facturas { get; set; }
		public DbSet<facturaDetalle> FacturaDetalles { get; set; }

		public AppDbContext() : base("name=DefaultConnection")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configuración de las relaciones
			modelBuilder.Entity<Factura>()
				.HasRequired(f => f.Cliente)
				.WithMany(c => c.Facturas)
				.HasForeignKey(f => f.idCliente);

			modelBuilder.Entity<facturaDetalle>()
				.HasRequired(fd => fd.Facturas)
				.WithMany(f => f.FacturaDetalles)
				.HasForeignKey(fd => fd.idFactura);

			modelBuilder.Entity<facturaDetalle>()
				.HasRequired(fd => fd.Producto)
				.WithMany(p => p.FacturaDetalles)
				.HasForeignKey(fd => fd.IdProducto);

			modelBuilder.Entity<Productos>()
				.HasRequired(p => p.Categoria)
				.WithMany(c => c.Producto)
				.HasForeignKey(p => p.idCategoria);
			modelBuilder.Entity<Usuarios>()
			.Property(u => u.Usuario)
			.IsRequired()
			.HasMaxLength(50);

			modelBuilder.Entity<Usuarios>()
				.Property(u => u.Clave)
				.IsRequired()
				.HasMaxLength(100);

			modelBuilder.Entity<Usuarios>()
				.Property(u => u.Rol)
				.IsRequired()
				.HasMaxLength(20);
		}
	}

}