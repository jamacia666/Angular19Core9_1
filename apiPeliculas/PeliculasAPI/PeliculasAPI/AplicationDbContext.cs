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
         //public DbSet<Pelicula> Peliculas { get; set; }
         //public DbSet<Comentario> Comentarios { get; set; }
    }
}
