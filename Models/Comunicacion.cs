using System.ComponentModel.DataAnnotations;

namespace Aprender.Models
{
    public class Comunicacion
    {
        [Key]
        public int Id { get; set; }
        public string? Asunto { get; set; }
        public string? Mensaje { get; set; }
        public DateTime? Fecha { get; set; }
        public string? UsuarioId { get; set; }        
        public string? Rol { get; set; } // Pudiendo ser Emisor o Receptor y luego el controlador deberá generar 2 registros por comunicacion

        //Tabla de la que obtiene a usuario emisor y receptor
        public virtual Usuario? Usuario { get; set; } = null!;
    }
}
