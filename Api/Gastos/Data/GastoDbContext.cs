using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Gastos.Models;

namespace Gastos.Data
{
    public class GastoDbContext : DbContext
    {
        public GastoDbContext(DbContextOptions<GastoDbContext> options) : base(options)
        {
        }

        public DbSet<Gasto> Gastos { get; set; }
    }
}
