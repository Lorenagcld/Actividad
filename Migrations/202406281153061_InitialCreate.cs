namespace PF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        idCliente = c.Int(nullable: false, identity: true),
                        Nombres = c.String(),
                        Apellidos = c.String(),
                        Cedula = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                    })
                .PrimaryKey(t => t.idCliente);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        idFactura = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        idCliente = c.Int(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        Iva = c.Double(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.idFactura)
                .ForeignKey("dbo.Clientes", t => t.idCliente, cascadeDelete: true)
                .Index(t => t.idCliente);
            
            CreateTable(
                "dbo.facturaDetalles",
                c => new
                    {
                        idFacturaDetalle = c.Int(nullable: false, identity: true),
                        idFactura = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Double(nullable: false),
                        Precio = c.Double(nullable: false),
                        Descuento = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.idFacturaDetalle)
                .ForeignKey("dbo.Facturas", t => t.idFactura, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.idFactura)
                .Index(t => t.IdProducto);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        idProducto = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CodigoBarras = c.String(),
                        idCategoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idProducto)
                .ForeignKey("dbo.Categories", t => t.idCategoria, cascadeDelete: true)
                .Index(t => t.idCategoria);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        idCategoria = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.idCategoria);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        idUsuario = c.Int(nullable: false, identity: true),
                        Clave = c.String(nullable: false, maxLength: 100),
                        Usuario = c.String(nullable: false, maxLength: 50),
                        Rol = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.idUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.facturaDetalles", "IdProducto", "dbo.Productos");
            DropForeignKey("dbo.Productos", "idCategoria", "dbo.Categories");
            DropForeignKey("dbo.facturaDetalles", "idFactura", "dbo.Facturas");
            DropForeignKey("dbo.Facturas", "idCliente", "dbo.Clientes");
            DropIndex("dbo.Productos", new[] { "idCategoria" });
            DropIndex("dbo.facturaDetalles", new[] { "IdProducto" });
            DropIndex("dbo.facturaDetalles", new[] { "idFactura" });
            DropIndex("dbo.Facturas", new[] { "idCliente" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Categories");
            DropTable("dbo.Productos");
            DropTable("dbo.facturaDetalles");
            DropTable("dbo.Facturas");
            DropTable("dbo.Clientes");
        }
    }
}
