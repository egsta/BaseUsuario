#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseUsuario.Models;

namespace BaseUsuario.Controllers
{
    public class AlquilerVentasController : Controller
    {
        private readonly BaseUsuarioContext _context;

        public AlquilerVentasController(BaseUsuarioContext context)
        {
            _context = context;
        }

        // GET: AlquilerVentas
        public async Task<IActionResult> Index()
        {
            var baseUsuarioContext = _context.AlquilerVenta.Include(a => a.Peliculas).Include(a => a.Usuarios);
            return View(await baseUsuarioContext.ToListAsync());
        }

        // GET: AlquilerVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilerVenta = await _context.AlquilerVenta
                .Include(a => a.Peliculas)
                .Include(a => a.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilerVenta == null)
            {
                return NotFound();
            }

            return View(alquilerVenta);
        }

        // GET: AlquilerVentas/Create
        public IActionResult Create()
        {
            ViewData["PeliculasId"] = new SelectList(_context.Set<Peliculas>(), "Id", "Id");
            ViewData["UsuariosId"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id");
            return View();
        }

        // POST: AlquilerVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,alq_com,precio,created_at,devolucion,PeliculasId,UsuariosId")] AlquilerVenta alquilerVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alquilerVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculasId"] = new SelectList(_context.Set<Peliculas>(), "Id", "Id", alquilerVenta.PeliculasId);
            ViewData["UsuariosId"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id", alquilerVenta.UsuariosId);
            return View(alquilerVenta);
        }

        // GET: AlquilerVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilerVenta = await _context.AlquilerVenta.FindAsync(id);
            if (alquilerVenta == null)
            {
                return NotFound();
            }
            ViewData["PeliculasId"] = new SelectList(_context.Set<Peliculas>(), "Id", "Id", alquilerVenta.PeliculasId);
            ViewData["UsuariosId"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id", alquilerVenta.UsuariosId);
            return View(alquilerVenta);
        }

        // POST: AlquilerVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,alq_com,precio,created_at,devolucion,PeliculasId,UsuariosId")] AlquilerVenta alquilerVenta)
        {
            if (id != alquilerVenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquilerVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilerVentaExists(alquilerVenta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculasId"] = new SelectList(_context.Set<Peliculas>(), "Id", "Id", alquilerVenta.PeliculasId);
            ViewData["UsuariosId"] = new SelectList(_context.Set<Usuarios>(), "Id", "Id", alquilerVenta.UsuariosId);
            return View(alquilerVenta);
        }

        // GET: AlquilerVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilerVenta = await _context.AlquilerVenta
                .Include(a => a.Peliculas)
                .Include(a => a.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilerVenta == null)
            {
                return NotFound();
            }

            return View(alquilerVenta);
        }

        // POST: AlquilerVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alquilerVenta = await _context.AlquilerVenta.FindAsync(id);
            _context.AlquilerVenta.Remove(alquilerVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilerVentaExists(int id)
        {
            return _context.AlquilerVenta.Any(e => e.Id == id);
        }
    }
}
