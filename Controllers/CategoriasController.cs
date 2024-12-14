using Api_snk.Services.Interfaces;
using Api_snk.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Snk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _service.GetAllAsync();
            return Ok(categorias);
        }
    }
}
