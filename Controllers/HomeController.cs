using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_06_Tjor_Korngold_Chinski.Models;

namespace TP_06_Tjor_Korngold_Chinski.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Paises()
    {
        ViewBag.Paises = BD.ListarPaises();
        return View();
    }
    public IActionResult Deportes()
    {
        ViewBag.Deportes = BD.ListarDeportes();
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
