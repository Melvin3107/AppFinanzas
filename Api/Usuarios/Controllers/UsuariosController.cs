using System;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Models;
using Usuarios.Services;
using System.Collections.Generic; // Necesario para IEnumerable<>


namespace Usuarios.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            return Ok(_usuarioService.GetUsuarios());
        }
    }
}
