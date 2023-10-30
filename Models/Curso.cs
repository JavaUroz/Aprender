using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Aprender.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre del curso.")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese descripción.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Ingrese el profesor.")]
        [Display(Name = "Profesor")]
        public string? ProfesorId { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Horario { get; set; }

        //Tabla de la que obtiene a usuario.profesor
        public virtual Usuario? Profesor { get; set; } = null!;

        //Tablas cuyas claves foraneas llaman a la tabla de cursos
        public virtual ICollection<Leccion> Lecciones { get; set; } = new List<Leccion>();
        public virtual ICollection<Examen> Cursos { get; set; } = new List<Examen>();
        public virtual ICollection<Progreso> Progresos { get; set; } = new List<Progreso>();
    }
}
