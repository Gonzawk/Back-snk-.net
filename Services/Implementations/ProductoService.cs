using Api_snk.Models;
using ApiSnk.Data;
using Api_snk.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Api_snk.Models.Api_snk.Models;

namespace ApiSnk.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDto>> GetAllAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Model = p.Model,
                    Color = p.Color,
                    Img_Url = p.Img_Url,
                    CategoriaNombre = p.Categoria != null ? p.Categoria.Nombre : null
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductoDto>> GetByCategoryAsync(int categoriaId)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Model = p.Model,
                    Color = p.Color,
                    Img_Url = p.Img_Url,
                    CategoriaNombre = p.Categoria != null ? p.Categoria.Nombre : null
                }).ToListAsync();
        }

        // Implementación del método para obtener productos paginados
        public async Task<IEnumerable<ProductoDto>> GetPaginatedAsync(int page, int pageSize)
        {
            // Calculamos el número de productos a saltar según la página actual
            var skip = (page - 1) * pageSize;

            // Obtenemos los productos paginados
            var productos = await _context.Productos
                .Include(p => p.Categoria) // Incluimos la categoría asociada al producto
                .Skip(skip)  // Saltamos los productos previos según la página
                .Take(pageSize)  // Tomamos solo los productos correspondientes a la página
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Model = p.Model,
                    Color = p.Color,
                    Img_Url = p.Img_Url,
                    CategoriaNombre = p.Categoria != null ? p.Categoria.Nombre : null
                }).ToListAsync();

            return productos;
        }
    }
}
