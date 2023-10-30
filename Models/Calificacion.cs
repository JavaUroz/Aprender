using System.ComponentModel.DataAnnotations;

namespace Aprender.Models
{
    public class Calificacion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe seleccionar el Exámen")]
        [Display(Name = "Exámen")]
        public int ExamenId { get; set; }
        [Required(ErrorMessage = "Indicar alumno.")]
        [Display(Name = "Estudiante")]
        public string? EstudianteId { get; set; }
        public float Nota { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }

        public virtual Usuario? Estudiante { get; set; } = null!;
        public virtual Examen? Examen  { get; set; } = null!;

    }
}
