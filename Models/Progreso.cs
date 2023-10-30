using System.ComponentModel.DataAnnotations;

namespace Aprender.Models
{
    public class Progreso
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Seleccione el estudiante")]
        [Display(Name = "Estudiante")]
        public string? EstudianteId { get; set; }
        [Required(ErrorMessage ="Indique el curso al que pertenece el estudiante.")]
        [Display(Name = "Curso")]
        public int cursoId { get; set; }
        public virtual Usuario? Estudiante { get; set; } = null!;
        public virtual Curso? Curso { get; set; } = null!;

    }
}
