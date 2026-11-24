using InventarioNet10.Models;

namespace InventarioNet10.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext db)
    {
        if (db.Productos.Any())
        {
            return;
        }

        db.Productos.AddRange(
            new Producto { Nombre = "Laptop Lenovo IdeaPad 3", Categoria = "Computadoras", Precio = 549.99m, Stock = 12, FechaRegistro = DateTime.Now.AddDays(-12) },
            new Producto { Nombre = "Monitor LG 24\" Full HD", Categoria = "Monitores", Precio = 129.50m, Stock = 18, FechaRegistro = DateTime.Now.AddDays(-10) },
            new Producto { Nombre = "Teclado mecánico Redragon Kumara", Categoria = "Accesorios", Precio = 39.99m, Stock = 35, FechaRegistro = DateTime.Now.AddDays(-8) },
            new Producto { Nombre = "Mouse Logitech M170", Categoria = "Accesorios", Precio = 14.75m, Stock = 60, FechaRegistro = DateTime.Now.AddDays(-6) },
            new Producto { Nombre = "Disco SSD Kingston 480 GB", Categoria = "Almacenamiento", Precio = 44.90m, Stock = 22, FechaRegistro = DateTime.Now.AddDays(-5) },
            new Producto { Nombre = "Router TP-Link Archer C6", Categoria = "Redes", Precio = 34.99m, Stock = 15, FechaRegistro = DateTime.Now.AddDays(-4) },
            new Producto { Nombre = "Impresora Epson EcoTank L3250", Categoria = "Impresoras", Precio = 199.00m, Stock = 7, FechaRegistro = DateTime.Now.AddDays(-3) },
            new Producto { Nombre = "Memoria USB SanDisk 64 GB", Categoria = "Almacenamiento", Precio = 9.99m, Stock = 80, FechaRegistro = DateTime.Now.AddDays(-2) }
        );

        db.SaveChanges();
    }
}
