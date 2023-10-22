namespace Aprender.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int ExamenId { get; set; }
        public string? EstudianteId { get; set; }
        public float Nota { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Usuario? Estudiante { get; set; } = null!;
        public virtual Examen? Examen  { get; set; } = null!;

    }
}
