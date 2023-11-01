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
using Microsoft.AspNetCore.Identity;

namespace Aprender.Controllers
{
    [Authorize]
    public class CalificacionesController : Controller
    {        
        private readonly AprenderDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public CalificacionesController(AprenderDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            var aprenderContext = _context.Calificacion.Include(c => c.Estudiante).Include(c => c.Examen).Include(c => c.Examen.Curso);
            return View(await aprenderContext.ToListAsync());
        }

        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .Include(c => c.Estudiante)
                .Include(c => c.Examen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }
        [Authorize(Roles = "Admin, Profesor, Secretario")]
        // GET: Calificaciones/Create
        public async Task<IActionResult> Create()
        {
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante");

            ViewData["EstudianteId"] = estudiantesSelectList;
            ViewData["ExamenId"] = new SelectList(
                _context.Set<Examen>()
                .Include(e => e.Curso)
                .Select(examen => new
                {
                    examen.Id,
                    Examen = $"{examen.Nombre} - {examen.Curso.Nombre} - {examen.Fecha}"
                }),
                "Id", "Examen");
            return View();
        }

        // POST: Calificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Profesor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExamenId,EstudianteId,Nota,Fecha")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante");

            ViewData["EstudianteId"] = estudiantesSelectList;
            ViewData["ExamenId"] = new SelectList(
                _context.Set<Examen>()
                .Include(e => e.Curso)
                .Select(examen => new
                {
                    examen.Id,
                    Examen = $"{examen.Nombre} - {examen.Curso.Nombre} - {examen.Fecha}"
                }),
                "Id", "Examen", calificacion.ExamenId);
            return View(calificacion);
        }
        [Authorize(Roles = "Admin, Profesor")]
        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante");

            ViewData["EstudianteId"] = estudiantesSelectList;
            ViewData["ExamenId"] = new SelectList(
                _context.Set<Examen>()
                .Include(e => e.Curso)
                .Select(examen => new
                {
                    examen.Id,
                    Examen = $"{examen.Nombre} - {examen.Curso.Nombre} - {examen.Fecha}"
                }),
                "Id", "Examen", calificacion.ExamenId);
            return View(calificacion);
        }
        [Authorize(Roles = "Admin, Profesor")]
        // POST: Calificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExamenId,EstudianteId,Nota,Fecha")] Calificacion calificacion)
        {
            if (id != calificacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.Id))
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
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante");

            ViewData["EstudianteId"] = estudiantesSelectList;
            ViewData["ExamenId"] = new SelectList(
                _context.Set<Examen>()
                .Include(e => e.Curso)
                .Select(examen => new
                {
                    examen.Id,
                    Examen = $"{examen.Nombre} - {examen.Curso.Nombre} - {examen.Fecha}"
                }),
                "Id", "Examen", calificacion.ExamenId);
            return View(calificacion);
        }
        [Authorize(Roles = "Admin, Profesor")]
        // GET: Calificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .Include(c => c.Estudiante)
                .Include(c => c.Examen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }
        [Authorize(Roles = "Admin, Profesor")]
        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calificacion == null)
            {
                return Problem("Entity set 'AprenderContext.Calificacion'  is null.");
            }
            var calificacion = await _context.Calificacion.FindAsync(id);
            if (calificacion != null)
            {
                _context.Calificacion.Remove(calificacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(int id)
        {
          return (_context.Calificacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
