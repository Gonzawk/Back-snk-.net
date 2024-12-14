namespace Api_snk.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Api_snk.Models
    {
        public class Producto
        {
            public int Id { get; set; }
            public string? Model { get; set; }
            public string? Color { get; set; }
            public string? Img_Url { get; set; }

            // Usar el atributo Column para mapear la columna en la base de datos
            [Column("categoria_Id")]
            public int CategoriaId { get; set; }

            public Categoria? Categoria { get; set; }
        }
    }

}
