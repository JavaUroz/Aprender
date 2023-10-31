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
    [Authorize(Roles = "Profesor, Admin, Secretario")]
    public class ProgresosController : Controller
    {
        private readonly AprenderDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ProgresosController(AprenderDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Progresos
        public async Task<IActionResult> Index()
        {
            var aprenderContext = _context.Progreso.Include(p => p.Curso).Include(p => p.Estudiante);
            return View(await aprenderContext.ToListAsync());
        }

        // GET: Progresos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Progreso == null)
            {
                return NotFound();
            }

            var progreso = await _context.Progreso
                .Include(p => p.Curso)
                .Include(p => p.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (progreso == null)
            {
                return NotFound();
            }

            return View(progreso);
        }
        
        // GET: Progresos/Create
        public async Task<IActionResult> Create()
        {
            ViewData["cursoId"] = new SelectList(_context.Curso, "Id", "Nombre");

            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante");

            ViewData["EstudianteId"] = estudiantesSelectList;

            return View();
        }

        // POST: Progresos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstudianteId,cursoId")] Progreso progreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cursoId"] = new SelectList(_context.Curso, "Id", "Nombre", progreso.cursoId);

            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante", progreso.EstudianteId);

            ViewData["EstudianteId"] = estudiantesSelectList;
            return View(progreso);
        }
        
        // GET: Progresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Progreso == null)
            {
                return NotFound();
            }

            var progreso = await _context.Progreso.FindAsync(id);
            if (progreso == null)
            {
                return NotFound();
            }
            ViewData["cursoId"] = new SelectList(_context.Curso, "Id", "Nombre", progreso.cursoId);

            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante", progreso.EstudianteId);
            ViewData["EstudianteId"] = estudiantesSelectList;
            return View(progreso);
        }

        // POST: Progresos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstudianteId,cursoId")] Progreso progreso)
        {
            if (id != progreso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgresoExists(progreso.Id))
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
            ViewData["cursoId"] = new SelectList(_context.Curso, "Id", "Nombre", progreso.cursoId);
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Estudiante");

            var estudiantesSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Estudiante = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Estudiante", progreso.EstudianteId);
            ViewData["EstudianteId"] = estudiantesSelectList;
            return View(progreso);
        }
        
        // GET: Progresos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Progreso == null)
            {
                return NotFound();
            }

            var progreso = await _context.Progreso
                .Include(p => p.Curso)
                .Include(p => p.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (progreso == null)
            {
                return NotFound();
            }

            return View(progreso);
        }
        
        // POST: Progresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Progreso == null)
            {
                return Problem("Entity set 'AprenderContext.Progreso'  is null.");
            }
            var progreso = await _context.Progreso.FindAsync(id);
            if (progreso != null)
            {
                _context.Progreso.Remove(progreso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgresoExists(int id)
        {
          return (_context.Progreso?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
