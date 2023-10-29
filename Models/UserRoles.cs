namespace Aprender.Models
{
    public class UserRoles
    { 
        public string? UserId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
