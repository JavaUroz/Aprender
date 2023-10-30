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
    public class LeccionesController : Controller
    {
        private readonly AprenderDbContext _context;

        public LeccionesController(AprenderDbContext context)
        {
            _context = context;
        }

        // GET: Lecciones
        public async Task<IActionResult> Index()
        {
            var aprenderContext = _context.Leccion.Include(l => l.Curso);
            return View(await aprenderContext.ToListAsync());
        }

        // GET: Lecciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leccion == null)
            {
                return NotFound();
            }

            var leccion = await _context.Leccion
                .Include(l => l.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leccion == null)
            {
                return NotFound();
            }

            return View(leccion);
        }

        // GET: Lecciones/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Curso, "Id", "Id");
            return View();
        }

        // POST: Lecciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Contenido,CursoId")] Leccion leccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "Id", "Id", leccion.CursoId);
            return View(leccion);
        }

        // GET: Lecciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leccion == null)
            {
                return NotFound();
            }

            var leccion = await _context.Leccion.FindAsync(id);
            if (leccion == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "Id", "Id", leccion.CursoId);
            return View(leccion);
        }

        // POST: Lecciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Contenido,CursoId")] Leccion leccion)
        {
            if (id != leccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeccionExists(leccion.Id))
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
            ViewData["CursoId"] = new SelectList(_context.Curso.Include(c => c.Cursos), "Id", "Id", leccion.CursoId);
            return View(leccion);
        }

        // GET: Lecciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leccion == null)
            {
                return NotFound();
            }

            var leccion = await _context.Leccion
                .Include(l => l.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leccion == null)
            {
                return NotFound();
            }

            return View(leccion);
        }

        // POST: Lecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leccion == null)
            {
                return Problem("Entity set 'AprenderContext.Leccion'  is null.");
            }
            var leccion = await _context.Leccion.FindAsync(id);
            if (leccion != null)
            {
                _context.Leccion.Remove(leccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeccionExists(int id)
        {
          return (_context.Leccion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
