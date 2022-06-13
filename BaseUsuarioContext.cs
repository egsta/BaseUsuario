using BaseUsuario.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseUsuario
{
    public class BaseUsuarioContext : DbContext
    {

        //public BaseUsuarioContext() : base("Prueba")
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<BaseUsuarioContext, EF6Console.Migrations.Configuration>());
        //}
       
        public BaseUsuarioContext(DbContextOptions<BaseUsuarioContext> options)
           : base(options)
        {
        }

       

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Rols> Rols { get; set; }
        public DbSet<Peliculas> Peliculas { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<AlquilerVenta> AlquilerVenta { get; set; }

       // public DbSet<GenerosPeliculas>  { get; set; }


    }
}
