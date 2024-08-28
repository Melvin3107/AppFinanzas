using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Usuarios.Data;
using Microsoft.Extensions.Configuration;
using Usuarios.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddDbContext<UsuarioDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Cambio a UseNpgsql para PostgreSQL

builder.Services.AddScoped<UsuarioService>();
//builder.Services.AddControllers(); // Configuración de servicios de controladores

builder.Services.AddControllers().AddNewtonsoftJson(); // Configurar servicios de controladores

var app = builder.Build(); // Construir la aplicación


// Configurar el puerto en el que la aplicación escuchará
app.Urls.Add("http://*:8581");

// Configurar la canalización de solicitudes HTTP
app.UseRouting();

// Middleware para autorización
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run(); // Ejecutar la aplicación
