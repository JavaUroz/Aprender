namespace Aprender.Models
{
    public class Examen
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public int CursoId { get; set; }
        public virtual Curso? Curso { get; set; } = null!;

        //Tablas cuyas claves foraneas llaman a la tabla de examenes
        public virtual ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
    }
}
