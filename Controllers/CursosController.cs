﻿using System;
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
    [Authorize(Roles = "Admin, Profesor, Secretario")]
    public class CursosController : Controller
    {
        private readonly AprenderDbContext _context;

        private readonly UserManager<Usuario> _userManager;

        public CursosController(AprenderDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            var aprenderContext = _context.Curso.Include(c => c.Profesor);
            return View(await aprenderContext.ToListAsync());
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Cursos/Create
        public async Task <IActionResult> Create()
        {
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Profesor");

            var profesoresSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Profesor = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Profesor");

            ViewData["ProfesorId"] = profesoresSelectList;
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,ProfesorId,Horario")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Profesor");

            var profesoresSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Profesor = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Profesor");

            ViewData["ProfesorId"] = profesoresSelectList;
            return View(curso);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Profesor");

            var profesoresSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Profesor = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Profesor");

            ViewData["ProfesorId"] = profesoresSelectList;
            return View(curso);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,ProfesorId,Horario")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
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
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Profesor");

            var profesoresSelectList = new SelectList(usersWithRole
                .Select(usuario => new
                {
                    usuario.Id,
                    Profesor = $"{usuario.Apellido}, {usuario.Nombre}"
                }), "Id", "Profesor");

            ViewData["ProfesorId"] = profesoresSelectList;
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Curso == null)
            {
                return Problem("Entity set 'AprenderContext.Curso'  is null.");
            }
            var curso = await _context.Curso.FindAsync(id);
            if (curso != null)
            {
                _context.Curso.Remove(curso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
          return (_context.Curso?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
