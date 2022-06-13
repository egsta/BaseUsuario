namespace BaseUsuario.Models
{
    public class Generos
    {
        public Generos()
        {
            Ids = new HashSet<Peliculas>();
        }

        public int Id { get; set; }
        public string? txt_desc { get; set; }

        public virtual ICollection<Peliculas> Ids { get; set; }
    }
}
