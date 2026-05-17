using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;


namespace PeliculasAPI.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController(AplicationDbContext context, IMapper mapper, IOutputCacheStore outputCacheStore, IAlmacenadorArchivos almacenadorArchivos

        ) : CustomBaseController(context, mapper, outputCacheStore, cacheTag)
    {
        private readonly AplicationDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly IOutputCacheStore outputCacheStore = outputCacheStore;
        private readonly IAlmacenadorArchivos almacenadorArchivos = almacenadorArchivos;
        private const string cacheTag="peliculas";
        private readonly string contenedor = "peliculas"           ;


        [HttpGet("{id:int}", Name = "ObtenerPeliculaPorId")]
        public ActionResult Get(int id)
        { 
           throw new NotImplementedException();
        }


        [HttpGet("PostGet")]
        public async Task<ActionResult<PeliculasPostGetDTO>> PostGet()
        {
            var cines = await context.Cines.ProjectTo<CineDTO>(mapper.ConfigurationProvider).ToListAsync();
            var generos = await context.Generos.ProjectTo<GeneroDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new PeliculasPostGetDTO() { Cines = cines, Generos = generos };

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculasCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);
            if (peliculaCreacionDTO.Poster != null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, peliculaCreacionDTO.Poster);
                pelicula.Poster = url;

            }
            AsignarOrdenActores(pelicula);
            context.Add(pelicula);
            await context.SaveChangesAsync();   
            await outputCacheStore.EvictByTagAsync(cacheTag,default);   
            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);    
            return CreatedAtRoute("ObtenerPeliculaPorId", new { id = pelicula.Id }, peliculaDTO);


        }
        private void AsignarOrdenActores(Pelicula pelicula)
        {
            if (pelicula.PeliculasActores != null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i;
                }
            }
        }


    }
}
