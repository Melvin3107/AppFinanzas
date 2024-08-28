using System;
using Microsoft.EntityFrameworkCore;
using Gastos.Data;
using Gastos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gastos.Services
{
    public class GastoService
    {
        private readonly GastoDbContext _context;

        public GastoService(GastoDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gasto> GetGastos()
        {
            return _context.Gastos;
        }
    }
}
