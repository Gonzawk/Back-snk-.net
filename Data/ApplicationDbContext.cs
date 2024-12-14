using Api_snk.Models;
using Api_snk.Models.Api_snk.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSnk.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}

