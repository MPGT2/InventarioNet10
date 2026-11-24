using InventarioNet10.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioNet10.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Producto>()
            .HasIndex(p => p.Nombre);

        modelBuilder.Entity<Producto>()
            .HasIndex(p => p.Categoria);
    }
}
