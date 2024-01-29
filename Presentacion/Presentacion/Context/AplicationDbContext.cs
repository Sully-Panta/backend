using Microsoft.EntityFrameworkCore;
using Models;

namespace Presentacion.Context
{
    public class AplicationDbContext : DbContext
    {
   

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        public DbSet<Curso> Curso { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Docenteperfil> Docenteperfil { get ; set; }
    }
}
