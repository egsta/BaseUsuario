using BaseUsuario.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using BaseUsuario.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace BaseUsuario.Controllers
{
    public class LoginController : Controller
    {

        private readonly BaseUsuarioContext _context;
        public LoginController( BaseUsuarioContext context)
        {
            
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModels objLoginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    var usuario = _context.Usuarios.Where(x => x.UserName == objLoginModel.UserName && x.Password == objLoginModel.Password).SingleOrDefault();// _context.Usuarios.Include("Rols").Where(x => x.UserName == objLoginModel.UserName && x.Password == objLoginModel.Password).SingleOrDefault();

                    if (usuario != null)
                    {
                        string Role = _context.Rols.Where(x => x.Id == usuario.RolsId).SingleOrDefault().Descripcion;
                        if (Role == null)
                        {
                            return NotFound(new JObject() { { "StatusCode", 404 }, { "Message", "Usuario no encontrado" } });
                        }
                        //var claims = new List<Claim>() {
                        //new Claim(ClaimTypes.NameIdentifier, Convert.ToString(usuario.id)),
                        //new Claim(ClaimTypes.Name, Convert.ToString(usuario.Nombre)),
                        //new Claim(ClaimTypes.Role, Convert.ToString(usuario.Rol_id))
                        //};

                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(usuario.Id)));
                        identity.AddClaim(new Claim(ClaimTypes.Name, Convert.ToString(usuario.Nombre)));
                        identity.AddClaim(new Claim(ClaimTypes.Role, Role));
                        identity.AddClaim(new Claim("Dato", "Valor"));
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                            new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddMinutes(1), IsPersistent = true });

                        // si tengo muchos roles asociados al usuario pudedo aregarlos con foreach, solo de referencia, no se aplica a este proyecto
                        //foreach(var Rol in usuario.Rols)
                        //{
                        //    identity.AddClaim(new Claim(ClaimTypes.Role, Rol.Descripcion));
                        //}

                        //SessionHelper.AddUserToSession(usuario.id.ToString());
                        return RedirectToAction("PeliculasAV", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Usuario o contraseña No validos";
                        
                        return View(objLoginModel);
                    }

                }
                catch (Exception e)
                {
                    ViewBag.Message = "Excepción no Controlada " + e;
                    return View();
                    //return BadRequest(e);
                }
            }
            ViewBag.Message = "Usuario o contraseña Salio por validacion de modelo";
            return View();

        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Register()
        {
            var rols = await _context.Rols.ToListAsync();
            ViewData["RolId"] = new SelectList(rols, "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("UserName,Password,Apellido, NroDocumento, Nombre,Email, RolsId")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                //ViewData["Rols"] = new SelectList(_context.Set<Rols>(), "Descripcion", "Descripcion");
                usuarios.SnActivo = -1;
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
                //return View();
            }
            ViewData["RolId"] = new SelectList(_context.Set<Rols>(), "Id", "Descripcion", usuarios.RolsId);
            return View();

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        public IActionResult AccessDenied()
        {
            
            return View();
        }
    }
}
