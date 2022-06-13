using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseUsuario.Models
{
    public class Peliculas
    {
        public Peliculas()
        {
            AlquilerVenta = new HashSet<AlquilerVenta>();
            CodGeneros = new HashSet<Generos>();
        }

        public int Id { get; set; }
        [Display(Name = "Peliculas")]
        public string? txt_desc { get; set; }
        [Display(Name = "Disponibles Alquiler")]
        public int? cant_disponibles_alquiler { get; set; }

        [Display(Name = "Disponibles Ventas")]
        public int? cant_disponibles_venta { get; set; }
        [Display(Name = "Precio Alquiler")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? precio_alquiler { get; set; }
        [Display(Name = "Precio Venta")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? precio_venta { get; set; }

        public virtual ICollection<AlquilerVenta> AlquilerVenta { get; set; }

        public virtual ICollection<Generos> CodGeneros { get; set; }
    }
}
