using System.ComponentModel.DataAnnotations;

namespace Aprender.Models
{
    public class Leccion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar un título")]
        [Display(Name = "Título")]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "Indique el contenido")]
        public string? Contenido { get; set; }
        [Required]
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        public virtual Curso? Curso { get; set; } = null!;
    }
}
