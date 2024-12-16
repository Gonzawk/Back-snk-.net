using Microsoft.EntityFrameworkCore;
using ApiSnk.Data;
using ApiSnk.Services.Implementations;
using Api_snk.Services.Interfaces;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Deshabilitar la globalización invariante para permitir el uso de culturas específicas
AppContext.SetSwitch("System.Globalization.Invariant", false);

// Configuración de DbContext para usar SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios adicionales
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();

// Agregar servicios de controladores
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Productos",
        Version = "v1",
        Description = "Una API para gestionar productos y categorías"
    });
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://frontendd-snk.vercel.app/", "https://frontendd-snk.vercel.app")  // Especifica los orígenes
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de localización
var supportedCultures = new[] { "es-ES", "en-US" }; // Asegúrate de que estas culturas sean válidas en tu entorno
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES", "es-ES"); // Cambiar la cultura predeterminada a 'es-ES'
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
});

// Configuración del servidor para escuchar en el puerto 5201
builder.WebHost.UseUrls("http://0.0.0.0:5201");

var app = builder.Build();

// Usar el middleware de CORS
app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // Habilitar Swagger
    app.UseSwaggerUI(); // Habilitar la interfaz Swagger
}

// Usar la configuración de localización (esto aplica para la globalización y las culturas)
app.UseRequestLocalization();

// Establecer manualmente el puerto HTTPS en el middleware
app.UseHttpsRedirection();

// Habilitar autorización
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
