using Microsoft.AspNetCore.Identity;

namespace Aprender.Models
{
    public class Usuario : IdentityUser
    {
        public int Dni { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Genero { get; set; } // Masculino o Femenino
        public string? Direccion { get; set; }
        public string Tipo { get; set; } = "Estudiante";// Profesor o Estudiante
        public byte? CertificadoAnalitico { get; set; }
        public byte? CopiaDni { get; set; }
        public bool Validado { get; set; }

        //Tablas cuyas claves foraneas llaman a la tabla de usuarios
        public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
        public virtual ICollection<Examen> Examenes { get; set; } = new List<Examen>();
        public virtual ICollection<Comunicacion> Comunicaciones { get; set; } = new List<Comunicacion>();
        public virtual ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
        public virtual ICollection<Progreso> Progresos { get; set; } = new List<Progreso>();
    }
}
