using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Aprender.Models
{
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = "El DNI es obligatorio,")]
        public int Dni { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string? Direccion { get; set; }

        //Tablas cuyas claves foraneas llaman a la tabla de usuarios
        public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
        public virtual ICollection<Examen> Examenes { get; set; } = new List<Examen>();
        public virtual ICollection<Comunicacion> Comunicaciones { get; set; } = new List<Comunicacion>();
        public virtual ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
        public virtual ICollection<Progreso> Progresos { get; set; } = new List<Progreso>();
    }
}
