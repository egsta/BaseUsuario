namespace BaseUsuario.Models
{
    public class DatosAlquilerVenta
    {
        public int Id { get; set; }
        public string usuario { get; set; }
        public int? alq_com { get; set; }
        public string pelicula { get; set; }
        public decimal? precio { get; set; }
        public DateTime created_at { get; set; }
        public int? devolucion { get; set; }

    }
}
