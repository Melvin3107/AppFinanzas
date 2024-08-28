using System;
using Microsoft.EntityFrameworkCore;
using Usuarios.Data;
using Usuarios.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Usuarios.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDbContext _context;

        public UsuarioService(UsuarioDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return _context.Usuarios;
        }
    } 

}
