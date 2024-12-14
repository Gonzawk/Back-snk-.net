using Api_snk.Models;
using Api_snk.Models.Api_snk.Models;

namespace Api_snk.Services.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> GetAllAsync();
        Task<IEnumerable<ProductoDto>> GetByCategoryAsync(int categoriaId);
        Task<IEnumerable<ProductoDto>> GetPaginatedAsync(int page, int pageSize); // Método de paginación
    }
}
