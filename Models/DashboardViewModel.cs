using Microsoft.EntityFrameworkCore;

namespace Aprender.Models
{
    public class DashboardViewModel
    {
        public int NumEstudiantes { get; set; }
        public int NumProfesores { get; set; }
        public int NumCursos { get; set; }         
        public int NumLecciones {  get; set; }
        public int NumExamenes {  get; set; }
        public int NumCalificaciones {  get; set; }
        public int NumAprobados { get; set; }
        public int NumReprobados { get; set; }
        public int NumProgresos { get; set; }

    }
}
