using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aprender.Data;
using Aprender.Models;
using Microsoft.AspNetCore.Authorization;

namespace Aprender.Controllers
{
    [Authorize]
    public class ComunicacionesController : Controller
    {
        private readonly AprenderDbContext _context;

        public ComunicacionesController(AprenderDbContext context)
        {
            _context = context;
        }

        // GET: Comunicaciones
        public async Task<IActionResult> Index()
        {
            var aprenderContext = _context.Comunicacion.Include(c => c.Usuario);
            return View(await aprenderContext.ToListAsync());
        }

        // GET: Comunicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comunicacion == null)
            {
                return NotFound();
            }

            var comunicacion = await _context.Comunicacion
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comunicacion == null)
            {
                return NotFound();
            }

            return View(comunicacion);
        }

        // GET: Comunicaciones/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id");
            return View();
        }

        // POST: Comunicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Asunto,Mensaje,Fecha,UsuarioId,Rol")] Comunicacion comunicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comunicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", comunicacion.UsuarioId);
            return View(comunicacion);
        }

        // GET: Comunicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comunicacion == null)
            {
                return NotFound();
            }

            var comunicacion = await _context.Comunicacion.FindAsync(id);
            if (comunicacion == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", comunicacion.UsuarioId);
            return View(comunicacion);
        }

        // POST: Comunicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Asunto,Mensaje,Fecha,UsuarioId,Rol")] Comunicacion comunicacion)
        {
            if (id != comunicacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comunicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComunicacionExists(comunicacion.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", comunicacion.UsuarioId);
            return View(comunicacion);
        }

        // GET: Comunicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comunicacion == null)
            {
                return NotFound();
            }

            var comunicacion = await _context.Comunicacion
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comunicacion == null)
            {
                return NotFound();
            }

            return View(comunicacion);
        }

        // POST: Comunicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comunicacion == null)
            {
                return Problem("Entity set 'AprenderContext.Comunicacion'  is null.");
            }
            var comunicacion = await _context.Comunicacion.FindAsync(id);
            if (comunicacion != null)
            {
                _context.Comunicacion.Remove(comunicacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComunicacionExists(int id)
        {
          return (_context.Comunicacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
