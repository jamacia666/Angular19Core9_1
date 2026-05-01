using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options):base(options) 
        {

        }
         public DbSet<Genero> Generos { get; set; }
         public DbSet<Actor> Actores { get; set; }
         public DbSet<Cine> Cines { get; set; }
        //public DbSet<Comentario> Comentarios { get; set; }
    }
}
