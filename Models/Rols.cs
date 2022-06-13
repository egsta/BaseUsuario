using System.ComponentModel.DataAnnotations;

namespace BaseUsuario.Models
{
    public class Rols
    {
     

        [Key]
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public int SnActivo { get; set; }

        public Usuarios usuarios { get; set; }
    }
}
