using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // las propiedaddes n x n se podrían configurar en las entidades, pero es más claro hacerlo aquí    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PeliculaGenero>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            modelBuilder.Entity<PeliculaActor>().HasKey(x => new { x.ActorId, x.PeliculaId });
            modelBuilder.Entity<PeliculaCine>().HasKey(x => new { x.CineId, x.PeliculaId });    
        }
         public DbSet<Genero> Generos { get; set; }
         public DbSet<Actor> Actores { get; set; }
         public DbSet<Cine> Cines { get; set; }
         public DbSet<PeliculaGenero> PeliculasGeneros { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
         public DbSet<PeliculaCine> PeliculasCines { get; set; }
    }
}
