using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaTecnicaP2.Models.Dtos;
using PruebaTecnicaP2.Models.PayLoads;
using PruebaTecnicaP2.Services;

namespace PruebaTecnicaP2.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPuntosService _puntosService;

    public HomeController(ILogger<HomeController> logger, IPuntosService puntosService)
    {
        _logger = logger;
        _puntosService = puntosService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<ResponseServiceDto<bool>>> CargarArchivo([FromBody] List<PuntosLoad> jsonInput)
    {
        ResponseServiceDto<bool> responseServiceDto = await _puntosService.CargarPuntos(jsonInput);
        return Ok(responseServiceDto);
    }


    [HttpGet]
    public async Task<ActionResult<ResponseServiceDto<List<PuntosDto>>>> ObtenerPuntos()
    {
        ResponseServiceDto<List<PuntosDto>> responseServiceDto = await _puntosService.ObtenerPuntos();
        return Ok(responseServiceDto);
    }
}
