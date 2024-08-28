using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Gastos.Data;
using Microsoft.Extensions.Configuration;
using Gastos.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddDbContext<GastoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el servicio GastoService
builder.Services.AddScoped<GastoService>();


builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();


// Configurar el puerto en el que la aplicación escuchará
app.Urls.Add("http://*:8582");

// Configurar la canalización de solicitudes HTTP
app.UseRouting();

// Agregar autenticación si es necesario
// app.UseAuthentication(); // Descomenta si necesitas autenticación

// Middleware para autorización
app.UseAuthorization();

// Mapea los controladores
app.MapControllers();

app.Run();
