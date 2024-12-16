using Microsoft.EntityFrameworkCore;
using ApiSnk.Data;
using ApiSnk.Services.Implementations;
using Api_snk.Services.Interfaces;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Deshabilitar la globalizaci�n invariante para permitir el uso de culturas espec�ficas
AppContext.SetSwitch("System.Globalization.Invariant", false);

// Configuraci�n de DbContext para usar SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios adicionales
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();

// Agregar servicios de controladores
builder.Services.AddControllers();

// Configuraci�n de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Productos",
        Version = "v1",
        Description = "Una API para gestionar productos y categor�as"
    });
});

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://frontendd-snk.vercel.app/", "https://frontendd-snk.vercel.app")  // Especifica los or�genes
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuraci�n de localizaci�n
var supportedCultures = new[] { "es-ES", "en-US" }; // Aseg�rate de que estas culturas sean v�lidas en tu entorno
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES", "es-ES"); // Cambiar la cultura predeterminada a 'es-ES'
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
});

// Configuraci�n del servidor para escuchar en el puerto 5201
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

// Usar la configuraci�n de localizaci�n (esto aplica para la globalizaci�n y las culturas)
app.UseRequestLocalization();

// Establecer manualmente el puerto HTTPS en el middleware
app.UseHttpsRedirection();

// Habilitar autorizaci�n
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Ejecutar la aplicaci�n
app.Run();
