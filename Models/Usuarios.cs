using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BaseUsuario.Models
{
    public class Usuarios
    {

        public Usuarios()
        {
            AlquilerVentas = new HashSet<AlquilerVenta>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Escriba su UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Escriba su Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Escriba su Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Escriba su Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Escriba su Apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Escriba su Documento")]
        [UsuarioExiste(ErrorMessage = "Usuario ya existe")]
        public string NroDocumento { get; set; }

        public int RolsId { get; set; }
       

        public virtual ICollection<AlquilerVenta> AlquilerVentas { get; set; }
        public int SnActivo { get; set; }

    }

    public class UsuarioExisteAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //IConfiguration configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();

            //var connectionString = configuration.GetConnectionString("BaseUsuarioContext");

            //var options = new DbContextOptionsBuilder<BaseUsuarioContext>()
            //                 .UseSqlServer(new SqlConnection(connectionString))
            //                 .Options;

            var options1 = new DbContextOptionsBuilder<BaseUsuarioContext>()
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Prueba")
                    .Options;

            var factory = new PooledDbContextFactory<BaseUsuarioContext>(options1);

            //using (var _context = new BaseUsuarioContext(options))
            using (var _context = factory.CreateDbContext())
            {
                string NroDocumento = (string)value;
                if (_context.Usuarios.Where(d => d.NroDocumento == NroDocumento).Count() > 0)
                {
                    return new ValidationResult("Usuario ya Existe");
                }
            }
            return ValidationResult.Success;

        }

    }
}
