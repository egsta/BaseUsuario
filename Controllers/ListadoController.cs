using BaseUsuario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseUsuario.Controllers
{
    public class ListadosController : Controller
    {

        private readonly BaseUsuarioContext _context;

        public ListadosController(BaseUsuarioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Prueba()
        {
            return View();
        }


        public async Task<IActionResult> ListaStock(int tipo)
        {
            ViewBag.Tipo = tipo;

            if (tipo == 1)
            {
                var sl = await _context.Peliculas.Where(b => b.cant_disponibles_alquiler > 0)
                    .Select(b => new Peliculas
                    {
                        Id = b.Id,
                        txt_desc = b.txt_desc,
                        cant_disponibles_alquiler = b.cant_disponibles_alquiler
                    })
                    .ToListAsync();
                return View(sl);
            }
            else if (tipo == 2)
            {
                var sl = await _context.Peliculas.Where(b => b.cant_disponibles_venta > 0)
                    .Select(b => new Peliculas
                    {
                        Id = b.Id,
                        txt_desc = b.txt_desc,
                        cant_disponibles_venta = b.cant_disponibles_venta
                    })
                    .ToListAsync();
                return View(sl);
            }
            else
            {
                return BadRequest("No se encontro el tipo");
            }



        }
        //Listado de pelicuals alquiladas

        //var peli = (from a in _context.AlquilerVenta
        //            join u in _context.Users on a.cod_usuario equals u.Id into ords
        //            from o in ords

        //            join p in _context.Peliculas on a.cod_pelicula equals p.Id into emps
        //            from q in emps
        //            select new
        //            {
        //                Id = a.Id,
        //                usuario = o.txt_nombre,
        //                alq_com = 1,
        //                pelicula = q.txt_desc, //"E", // q.txt_desc,
        //                precio = a.precio,
        //                created_at = a.created_at,
        //                devolucion = a.devolucion
        //            });
        public class modelo_listaPelAlq
        {
            public int Id { get; set; }
            public string Peli { get; set; }
            public string Usuario { get; set; }

            public string Precio { get; set; }
            public decimal Count { get; set; }
            public Decimal Suma { get; set; }
            public DateTime Fecha { get; set; }
        }

        public async Task<IActionResult> ListaPelAlq()
        {
            var sl = (from a in _context.AlquilerVenta
                      where a.alq_com == 1
                      join p in _context.Peliculas on a.PeliculasId equals p.Id into ords
                      from o in ords
                      join u in _context.Usuarios on a.UsuariosId equals u.Id into user
                      from us in user
                      select new modelo_listaPelAlq
                      {
                          Id = a.Id,
                          Peli = o.txt_desc,
                          Usuario = us.Nombre,
                          Precio = o.precio_alquiler.ToString(),
                          Fecha = a.created_at
                      });

            return View(sl);

        }
        // Listado pelicuals no devueltas
        public async Task<IActionResult> ListaPelAlqNoDvueltas()
        {
            var sl = (from a in _context.AlquilerVenta
                      where a.devolucion == -1 && a.alq_com == 1
                      join p in _context.Peliculas on a.PeliculasId equals p.Id into ords
                      from o in ords
                      join u in _context.Usuarios on a.UsuariosId equals u.Id into user
                      from us in user
                      select new modelo_listaPelAlq
                      {
                          Id = a.Id,
                          Peli = o.txt_desc,
                          Usuario = us.Nombre
                      });

            return View(sl);

        }
        //var suma = (from sol in solicitud.Desplazamientos
        //            from con in sol.ConcepTrayec
        //            group con by new { con.IdConcepto, con.IdCategoria } into g
        //            select new
        //            {
        //                IdConcepto = g.Key.IdConcepto,
        //                IdCategoria = g.Key.IdCategoria,
        //                Valor = g.Sum(x => x.Valor)
        //            }).ToList();
        public IActionResult ListaPelisRecaudado()
        {
            var sl = (from alq in _context.AlquilerVenta
                      group alq by new { alq.PeliculasId } into g
                      select new modelo_listaPelAlq
                      {
                          Peli = g.Key.PeliculasId.ToString(),
                          Suma = (decimal)g.Sum(x => x.precio),
                          Count = g.Count()


                      });
            return View(sl);
        }
    }
}