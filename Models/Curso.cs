using System.Data;

namespace Aprender.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ProfesorId { get; set; }
        public DateTime? Horario { get; set; }

        //Tabla de la que obtiene a usuario.profesor
        public virtual Usuario? Profesor { get; set; } = null!;

        //Tablas cuyas claves foraneas llaman a la tabla de cursos
        public virtual ICollection<Leccion> Lecciones { get; set; } = new List<Leccion>();
        public virtual ICollection<Examen> Cursos { get; set; } = new List<Examen>();
        public virtual ICollection<Progreso> Progresos { get; set; } = new List<Progreso>();
    }
}
