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
    private readonly IWebHostEnvironment _webHost;
    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHost)
    {
        _logger = logger;
        _webHost = webHost;
    }


    public IActionResult Index()
    {
        //const string[] 
        return View();
    }
    public IActionResult Historia()
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
            ViewBag.ListaPaises = BD.ListarPaises();
            return View("DetalleDeporte");
        }
        else
            return RedirectToAction("Error");
    }
    public IActionResult VerDetalleDeportista(int idDeportista)
    {
        ViewBag.DetalleDeportista = BD.VerInfoDeportista(idDeportista);
        if (ViewBag.DetalleDeportista != null)
        {
            ViewBag.DetalleDeporte = BD.VerInfoDeporte(ViewBag.DetalleDeportista.IdDeporte);
            ViewBag.DetallePais = BD.VerInfoPais(ViewBag.DetalleDeportista.IdPais);
            return View("DetalleDeportista");
        }
        else
            return RedirectToAction("Error");
    }
    public IActionResult VerDetallePais(int idPais)
    {
        ViewBag.DetallePais = BD.VerInfoPais(idPais);
        if (ViewBag.DetallePais != null)
        {
            ViewBag.ListaDeportistas = BD.ListarDeportistasPorPais(idPais);
            ViewBag.ListaDeportes = BD.ListarDeportes();
            return View("DetallePais");
        }
        else
            return RedirectToAction("Error");
    }


    public IActionResult AgregarDeporte()
    {
        return View("FormularioCargaDeportes");
    }
    public IActionResult AgregarDeportista()
    {
        ViewBag.ListarDeportes = BD.ListarDeportes();
        ViewBag.ListarPaises = BD.ListarPaises();

        return View("FormularioCargaDeportistas");
    }
    public IActionResult AgregarPais()
    {
        return View("FormularioCargaPaises");
    }


    public IActionResult EliminarDeportista(int idCandidato)
    {
        BD.EliminarDeportista(idCandidato);
        return View("Index");
    }


    [HttpPost]
    public IActionResult GuardarDeporte(Deporte dep)
    {
        bool parametrosExisten = !String.IsNullOrEmpty(dep.Nombre) && !String.IsNullOrEmpty(dep.Foto) && !String.IsNullOrEmpty(dep.Banner);
        ViewBag.Dep = dep;

        if (parametrosExisten)
        {
            BD.AgregarDeporte(dep);
            return RedirectToAction("Index");
        }
        else
           ViewBag.Error = "Faltan completar cosas";

        return View("FormularioCargaDeporte");
    }
    [HttpPost]
    public IActionResult GuardarDeportista(Deportista dep)
    {
        bool parametrosExisten = !String.IsNullOrEmpty(dep.Apellido) && !String.IsNullOrEmpty(dep.Nombre) && dep.FechaNacimiento != DateTime.MinValue && dep.IdPais > 0 && dep.IdDeporte > 0;
        bool fkExisten;
        ViewBag.Dep = dep;

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
    [HttpPost]
    public IActionResult GuardarPais(Pais pai)
    {
        bool parametrosExisten = !String.IsNullOrEmpty(pai.Nombre) && !String.IsNullOrEmpty(pai.Bandera);
        ViewBag.Pai = pai;

        if (parametrosExisten)
        {
            BD.AgregarPais(pai);
            return RedirectToAction("Index");
        }
        else
           ViewBag.Error = "Faltan completar cosas";

        return View("FormularioCargaPaises");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult PoliticaPrivacidad()
    {
        return View();
    }
    public IActionResult InformacionLegal()
    {
        return View();
    }
}
