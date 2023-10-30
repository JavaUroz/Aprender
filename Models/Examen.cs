using System.ComponentModel.DataAnnotations;

namespace Aprender.Models
{
    public class Examen
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        public string? Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        public virtual Curso? Curso { get; set; } = null!;

        //Tablas cuyas claves foraneas llaman a la tabla de examenes
        public virtual ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
    }
}
