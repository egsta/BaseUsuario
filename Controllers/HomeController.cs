using BaseUsuario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BaseUsuario.Helper;
using Microsoft.Data.SqlClient;

namespace BaseUsuario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BaseUsuarioContext _context;

        public HomeController(ILogger<HomeController> logger, BaseUsuarioContext context)
        {
            _logger = logger;
            _context = context;
        }
      
        public IActionResult Index()
        {
           // var ususarios = _context.Usuarios.ToList();// _context.Usuarios.Include("Rols").ToList();

            return RedirectToAction("PeliculasAV");
        }
 

        [Authorize]
        public IActionResult PeliculasAV()
        {
            ViewBag.Usuario = Helper.SessionHelper.GetName(User);
            var peliculas = _context.Peliculas.ToList();
            return View(peliculas);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel error)
        {
            return View(error);//new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        }



        // GET: Peliculas/Alquiler/5
        [Authorize]
        public async Task<IActionResult> Alquiler(int? id, int? tipo_ope)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculas = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peliculas == null)
            {
                return NotFound();
            }

            DatosAlquilerVenta alquilerVenta = new DatosAlquilerVenta
            {
                Id = peliculas.Id,
                usuario = SessionHelper.GetNameIdentifier(User),
                pelicula = peliculas.txt_desc,
                precio = peliculas.precio_alquiler
            };
            if ((tipo_ope == 1 && peliculas.cant_disponibles_alquiler == 0) || (tipo_ope == 2 && peliculas.cant_disponibles_venta == 0))
            {


                return RedirectToAction("Error", "Home", 1, "Lo sentimos no tenemos esa pelicula disponible");
            }
            ViewBag.Tipo = tipo_ope;
            //var operacion = tipo_ope == 1 ? "Alquiler" :
            //                tipo_ope == 2 ? "Comprar" :
            //                tipo_ope == 3 ? "Devolver" : "";

           
            


            ViewBag.Operacion = tipo_ope == 1 ? "Alquiler" :
                                tipo_ope == 2 ? "Comprar" :
                                tipo_ope == 3 ? "Devolver" : "";

            return View(alquilerVenta);
        }

        [Authorize]
        // POST: Peliculas/Alquiler/5
        [HttpPost, ActionName("AlquilerConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlquilerConfirmed(int id, int alq_com)
        {
            var peliculas = await _context.Peliculas.FindAsync(id);
            if (peliculas == null)
            {
                var model = new ErrorViewModel();
                model.RequestId = "Lo sentimos no enontramos la Pelicula";
                return View("Error", model);
            }


            _context.AlquilerVenta.AddRange(
                new AlquilerVenta
                {
                    UsuariosId = _context.Usuarios.Where(x => x.Id == Convert.ToInt16(SessionHelper.GetNameIdentifier(User))).FirstOrDefault().Id,
                    alq_com = alq_com,
                    PeliculasId = _context.Peliculas.Where(x => x.Id == id).FirstOrDefault().Id,
                    precio = peliculas.precio_alquiler,
                    devolucion = -1
                });
            
            var result = await _context.SaveChangesAsync();

            //AlquilerVenta alquilerVenta = new AlquilerVenta
            //{
            //    Usuarios = _context.Usuarios.Where(x => x.Id == Convert.ToInt16(SessionHelper.GetNameIdentifier(User))).FirstOrDefault(),
            //    alq_com = alq_com,
            //    Peliculas = _context.Peliculas.Where(x => x.Id == id).FirstOrDefault(),
            //    precio = peliculas.precio_alquiler,
            //    devolucion = -1
            //};

            //var sqlcomm1 = "insert into AlquilerVenta (UsuariosId, alq_com, PeliculasId, precio, devolucion) Values ("
            //           + alquilerVenta.Usuarios.Id + ","
            //           + alquilerVenta.alq_com + ","
            //           + alquilerVenta.Peliculas.Id + "," 
            //           + alquilerVenta.precio.ToString().Replace(",",".") + ","
            //           + alquilerVenta.devolucion + ")";

            //var result = await _context.Database.ExecuteSqlRawAsync(sqlcomm1);

            if (result == 1)
            {
                if (alq_com == 1)
                {
                    peliculas.cant_disponibles_alquiler--;
                }
                else
                {
                    peliculas.cant_disponibles_venta--;
                }
                try
                {
                    _context.Update(peliculas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculasExists(peliculas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(PeliculasAV));
            }
            //    _context.Update(alquilerVenta);
            //await _context.SaveChangesAsync();

            //using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = Prueba;Trusted_Connection=true;"))
            //{
            //    try
            //    {
            //        con.Open();
            //        string _precio = alquilerVenta.precio.ToString().Replace(",", ".");

            //        var sqlcomm = "insert into AlquilerVenta (cod_usuario, alq_com, cod_pelicula, precio, devolucion) Values ("
            //            + alquilerVenta.Usuarios + ","
            //            + alquilerVenta.alq_com + ","
            //            + alquilerVenta.Peliculas + "," +
            //            _precio + ","
            //            + alquilerVenta.devolucion + ")";

            //        SqlCommand com = new SqlCommand(sqlcomm, con); // Create a object of SqlCommand class
            //        var result = com.ExecuteNonQuery();
            //        if (result == 1)
            //        {
            //            if (alq_com == 1)
            //            {
            //                peliculas.cant_disponibles_alquiler--;
            //            }
            //            else
            //            {
            //                peliculas.cant_disponibles_venta--;
            //            }
            //            try
            //            {
            //                _context.Update(peliculas);
            //                await _context.SaveChangesAsync();
            //            }
            //            catch (DbUpdateConcurrencyException)
            //            {
            //                if (!PeliculasExists(peliculas.Id))
            //                {
            //                    return NotFound();
            //                }
            //                else
            //                {
            //                    throw;
            //                }
            //            }
            //            return RedirectToAction(nameof(PeliculasAV));
            //     }
            else
            {
                var model = new ErrorViewModel();
                model.RequestId = "Lo sentimos la operacion no termino correctamente";
                return RedirectToAction("Error" , "Home", model);
            }
               

        }
        [Authorize]
        // Devolver pelicula
        public async Task<IActionResult> Devolver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculas = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (peliculas == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return View(peliculas);

        }
        [Authorize]
        [HttpPost, ActionName("DevolverConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DevolverConfirmed(int id)
        {


            var alquilerVentas = await _context.AlquilerVenta.FirstOrDefaultAsync(m => m.PeliculasId == id &&
                                                                                        m.devolucion == -1 &&
                                                                                        m.UsuariosId == Convert.ToInt32(SessionHelper.GetNameIdentifier(User)) &&
                                                                                        m.alq_com == 1 );
            //var alquiler1 = await _context.AlquilerVenta.FirstOrDefaultAsync(m => m.devolucion == -1);
            //var alquiler2 = await _context.AlquilerVenta.FirstOrDefaultAsync(m=> m.UsuariosId == Convert.ToInt32(SessionHelper.GetNameIdentifier(User)));
            //var alquiler3 = await _context.AlquilerVenta.FirstOrDefaultAsync(m => m.alq_com == 1);
            if (alquilerVentas==null)
            {
                // No dan todas las condiciones
                var model = new ErrorViewModel();
                model.RequestId = "Error 1";
                
                model.ErrorDescription = "Lo sentimos la operacion no termino correctamente Ud. no alquilo esa pelicula";
                return RedirectToAction("Error", "Home", model);
            }

            alquilerVentas.devolucion = 1;

            var peliculas = await _context.Peliculas
               .FirstOrDefaultAsync(m => m.Id == id);
            peliculas.cant_disponibles_alquiler++;

            // aca procedimiento para buscar el usuario que devolvio la pelicula y pasarla a "1" (devuelta) en devolucion
            try
            {

                _context.Update(peliculas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculasExists(peliculas.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(PeliculasAV));
        }


        private bool PeliculasExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}