using System.ComponentModel.DataAnnotations.Schema;

namespace BaseUsuario.Models
{
    public class AlquilerVenta
    {

        

        public int Id { get; set; }
     
        public int? alq_com { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? precio { get; set; }
        public DateTime created_at { get; set; }
        public int? devolucion { get; set; }
        public int PeliculasId { get; set; }
        public int UsuariosId { get; set; }
        public virtual Peliculas Peliculas { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
