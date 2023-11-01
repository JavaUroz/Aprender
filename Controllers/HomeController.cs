using Aprender.Data;
using Aprender.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aprender.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AprenderDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AprenderDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Dashboard()
        {
            int numEstudiantes = _context.UserRoles.Where(u => u.RoleId == "e9bd49da-9494-4cb0-ab1a-e7ba3b0a639b").Count();
            int numProfesores = _context.UserRoles.Where(u => u.RoleId == "fd08ecd4-294b-4987-8882-a853ec110213").Count();
            int numCursos = _context.Curso.Count();
            int numLecciones = _context.Leccion.Count();
            int numExamenes = _context.Examen.Count();
            int numCalificaciones = _context.Calificacion.Count();
            int numProgresos = _context.Progreso.Count();

            var model = new DashboardViewModel
            {
                //NumEstudiantes = numEstudiantes,
            };

            return View();
        }
    }
}