using System;
using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
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

    public IActionResult Creditos()
    {
        return View();
    }


    public IActionResult VerDetalleDeporte(int idDeporte)
    {
        ViewBag.DetalleDeporte = BD.VerInfoDeporte(idDeporte);
        if (ViewBag.DetalleDeporte != null)
        {
            ViewBag.ListaDeportistas = BD.ListarDeportistasPorDeporte(idDeporte);
            return View("DetalleDeporte");
        }
        else
            return RedirectToAction("Error");
    }

    public IActionResult VerDetallePais(int idPais)
    {
        ViewBag.DetallePais = BD.VerInfoPais(idPais);
        if (ViewBag.DetalleDeporte != null)
        {
            ViewBag.ListaDeportistas = BD.ListarDeportistasPorPais(idPais);
            return View("DetallePais");
        }
        else
            return RedirectToAction("Error");
    }

    public IActionResult VerDetalleDeportista(int idDeportista)
    {
        ViewBag.DetalleDeportista = BD.VerInfoDeportista(idDeportista);
        if (ViewBag.DetalleDeporte != null)
            return View("DetalleDeportista");
        else
            return RedirectToAction("Error");
    }


    public IActionResult AgregarDeportista()
    {
        ViewBag.ListarDeportes = BD.ListarDeportes();
        ViewBag.ListarPaises = BD.ListarPaises();

        return View("FormularioCargaDeportistas");
    }

    public IActionResult EliminarDeportista(int idCandidato)
    {
        BD.EliminarDeportista(idCandidato);
        return View("Index");
    }

    [HttpPost]
    public IActionResult GuardarDeportista(Deportista dep)
    {
        bool parametrosExisten = dep.Apellido != String.Empty && dep.Nombre != String.Empty && dep.FechaNacimiento != DateTime.MinValue && dep.IdPais > 0 && dep.IdDeporte > 0;
        bool fkExisten;

        if (parametrosExisten)
        {
            fkExisten = BD.VerInfoPais(dep.IdPais) != null && BD.VerInfoDeporte(dep.IdDeporte) != null;
            if (fkExisten)
            {
                BD.AgregarDeportista(dep);
                return RedirectToAction("Index");
            }
            else
                ViewBag.Error = "El país/deporte no existe(n)";
        }
        else
           ViewBag.Error = "Faltan completar cosas";

        ViewBag.ListarDeportes = BD.ListarDeportes();
        ViewBag.ListarPaises = BD.ListarPaises();
        return View("FormularioCargaDeportistas");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
