using System.Text.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Services;

namespace Pokedex.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPokeService _service;

    public HomeController(ILogger<HomeController> logger, IPokeService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Index()
    {
        var pokemons = _service.GetPokemons();
        ViewData["Tipos"] = _service.GetTipos();  
        return View(pokemons);
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
