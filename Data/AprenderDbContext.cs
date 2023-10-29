using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aprender.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aprender.Data
{
    public class AprenderDbContext : IdentityDbContext <Usuario>
    {
        public AprenderDbContext (DbContextOptions<AprenderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aprender.Models.Calificacion> Calificacion { get; set; } = default!;

        public DbSet<Aprender.Models.Comunicacion>? Comunicacion { get; set; }

        public DbSet<Aprender.Models.Curso>? Curso { get; set; }

        public DbSet<Aprender.Models.Examen>? Examen { get; set; }

        public DbSet<Aprender.Models.Leccion>? Leccion { get; set; }

        public DbSet<Aprender.Models.Progreso>? Progreso { get; set; }
    }
}
