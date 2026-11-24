using InventarioNet10.Data;
using InventarioNet10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioNet10.Controllers;

public class ProductosController : Controller
{
    private readonly AppDbContext _db;

    public ProductosController(AppDbContext db)
    {
        _db = db;
    }

    // GET: Productos
    public async Task<IActionResult> Index(string? buscar, string? categoria)
    {
        var consulta = _db.Productos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            consulta = consulta.Where(p => p.Nombre.Contains(buscar) || p.Categoria.Contains(buscar));
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            consulta = consulta.Where(p => p.Categoria == categoria);
        }

        ViewBag.Buscar = buscar;
        ViewBag.Categoria = categoria;
        ViewBag.Categorias = await _db.Productos
            .Select(p => p.Categoria)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
        ViewBag.TotalProductos = await _db.Productos.CountAsync();
        ViewBag.TotalStock = await _db.Productos.SumAsync(p => (int?)p.Stock) ?? 0;
        ViewBag.ValorInventario = await _db.Productos
            .SumAsync(p => (decimal?)p.Precio * p.Stock) ?? 0m;

        var productos = await consulta.OrderBy(p => p.Nombre).ToListAsync();
        return View(productos);
    }

    // GET: Productos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _db.Productos.FirstOrDefaultAsync(p => p.Id == id);
        if (producto == null)
        {
            return NotFound();
        }

        return View(producto);
    }

    // GET: Productos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Productos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Categoria,Precio,Stock,FechaRegistro")] Producto producto)
    {
        if (!ModelState.IsValid)
        {
            return View(producto);
        }

        producto.FechaRegistro = DateTime.Now;
        _db.Add(producto);
        await _db.SaveChangesAsync();
        TempData["Mensaje"] = "Producto registrado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Productos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _db.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        return View(producto);
    }

    // POST: Productos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Categoria,Precio,Stock,FechaRegistro")] Producto producto)
    {
        if (id != producto.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(producto);
        }

        try
        {
            _db.Update(producto);
            await _db.SaveChangesAsync();
            TempData["Mensaje"] = "Producto actualizado correctamente.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _db.Productos.AnyAsync(p => p.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Productos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _db.Productos.FirstOrDefaultAsync(p => p.Id == id);
        if (producto == null)
        {
            return NotFound();
        }

        return View(producto);
    }

    // POST: Productos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var producto = await _db.Productos.FindAsync(id);
        if (producto != null)
        {
            _db.Productos.Remove(producto);
            await _db.SaveChangesAsync();
            TempData["Mensaje"] = "Producto eliminado correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }
}
