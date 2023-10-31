using Aprender.Data;
using Aprender.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aprender.Controllers
{
    public class SecretariosController : Controller
    {
        private readonly AprenderDbContext _context;

        private readonly UserManager<Usuario> _userManager;

        public SecretariosController(AprenderDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: SecretariosController
        public async Task<ActionResult> Index()
        {
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Secretario");

            return View(usersWithRole);
        }

        // GET: SecretariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SecretariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecretariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SecretariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SecretariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SecretariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SecretariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
