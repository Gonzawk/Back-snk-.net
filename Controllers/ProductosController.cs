using Api_snk.Services.Interfaces;
using Api_snk.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Snk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _service.GetAllAsync();
            return Ok(productos);
        }
    }
}
