using System.ComponentModel.DataAnnotations;

namespace Aprender.Models
{
    public class Progreso
    {
        [Key]
        public int Id { get; set; }
        public string? EstudianteId { get; set; }
        public int cursoId { get; set; }
        public virtual Usuario? Estudiante { get; set; } = null!;
        public virtual Curso? Curso { get; set; } = null!;

    }
}
