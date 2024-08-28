using System; 
using Microsoft.AspNetCore.Mvc;
using Gastos.Models;
using Gastos.Services;
using System.Collections.Generic; // Necesario para IEnumerable<>
using Microsoft.Extensions.Configuration;



namespace Gastos.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class GastosController : ControllerBase
    {
        private readonly GastoService _gastoService;

        public GastosController(GastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Gasto>> Get()
        {
            return Ok(_gastoService.GetGastos());
        }

    }
}
