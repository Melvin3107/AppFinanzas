using System; 
using Microsoft.AspNetCore.Mvc;
using Gastos.Models;
using Gastos.Services;
using System.Collections.Generic; // Necesario para IEnumerable<>
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Gastos.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class GastosController : ControllerBase
    {
        private readonly GastoService _gastoService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GastosController(GastoService gastoService)
        {
            _gastoService = gastoService;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Gasto>> Get()
        {
            return Ok(_gastoService.GetGastos());
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string projectId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var url = "http://172.18.0.5:8090/api/v1/bom"; // Cambia esto según tu URL
            var apiKey = _configuration["ApiKey"]; // Asegúrate de definir la clave en tu appsettings.json

            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(projectId), "project");

            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            form.Add(fileContent, "bom", file.FileName);

            _httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);

            var response = await _httpClient.PutAsync(url, form);

            if (response.IsSuccessStatusCode)
            {
                return Ok("File uploaded successfully.");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, responseBody);
            }
        }
    }
}
