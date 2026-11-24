using System.Diagnostics;
using InventarioNet10.Data;
using InventarioNet10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioNet10.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalProductos = await _db.Productos.CountAsync();
        ViewBag.TotalStock = await _db.Productos.SumAsync(p => (int?)p.Stock) ?? 0;
        ViewBag.TotalCategorias = await _db.Productos.Select(p => p.Categoria).Distinct().CountAsync();
        ViewBag.Ultimos = await _db.Productos
            .OrderByDescending(p => p.Id)
            .Take(5)
            .ToListAsync();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
