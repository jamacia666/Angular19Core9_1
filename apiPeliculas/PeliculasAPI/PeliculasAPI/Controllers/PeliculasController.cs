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


        [HttpGet("landing")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<LandingPageDTO>> Get()
        {
            var top = 6;
            var hoy = DateTime.Today;
            var proximosExtrenos = await context.Peliculas
                .Where(p => p.FechaLanzamiento > hoy)
                .OrderBy(p => p.FechaLanzamiento)
                .Take(top)
                .ProjectTo<PeliculaDTO>(mapper.ConfigurationProvider)
                .ToListAsync(); 
            var enCines = await context.Peliculas
                .Where(p => p.PeliculasCines.Select(pc => pc.PeliculaId).Contains(p.Id))
                .OrderBy(p => p.FechaLanzamiento)
                .Take(top)
                .ProjectTo<PeliculaDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
            var resultado = new LandingPageDTO();
            resultado.EnCines = enCines;
            resultado.ProximosExtrenos = proximosExtrenos;
            return resultado;
        }

        [HttpGet("{id:int}", Name = "ObtenerPeliculaPorId")]
        public async Task<ActionResult<PeliculaDetallesDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas
                .ProjectTo<PeliculaDetallesDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            //var promedioVoto = 0.0;
            //var usuarioVoto = 0;

            //if (await context.RatingsPeliculas.AnyAsync(r => r.PeliculaId == id))
            //{
            //    promedioVoto = await context.RatingsPeliculas.Where(r => r.PeliculaId == id)
            //        .AverageAsync(r => r.Puntuacion);

            //    if (HttpContext.User.Identity!.IsAuthenticated)
            //    {
            //        var usuarioId = await servicioUsuarios.ObtenerUsuarioId();

            //        var ratingDB = await context.RatingsPeliculas
            //            .FirstOrDefaultAsync(r => r.UsuarioId == usuarioId && r.PeliculaId == id);

            //        if (ratingDB is not null)
            //        {
            //            usuarioVoto = ratingDB.Puntuacion;
            //        }
            //    }
            //}

            //pelicula.PromedioVoto = promedioVoto;
            //pelicula.VotoUsuario = usuarioVoto;

            return pelicula;
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

        [HttpGet("PutGet/{id:int}")]    
        public async Task<ActionResult<PeliculasPutGetDTO>> PutGet(int id)
        {
            var pelicula = await context.Peliculas
                .ProjectTo<PeliculaDetallesDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pelicula is null)
            {
                return NotFound();
            }
            var generosSeleccionadosIds = pelicula.Generos.Select(g => g.Id).ToList();
            var generosNoSeleccionados = await context.Generos
                .Where(g => !generosSeleccionadosIds.Contains(g.Id))
                .ProjectTo<GeneroDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
            var cinesSeleccionadosIds = pelicula.Cines.Select(c => c.Id).ToList();
            var cinesNoSeleccionados = await context.Cines
                .Where(c => !cinesSeleccionadosIds.Contains(c.Id))
                .ProjectTo<CineDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
            var respuesta= new PeliculasPutGetDTO();
            respuesta.Pelicula = pelicula;
            respuesta.GenerosSeleccionados = pelicula.Generos;
            respuesta.GenerosNoSeleccionados = generosNoSeleccionados;
            respuesta.CinesSeleccionados = pelicula.Cines;
            respuesta.CinesNoSeleccionados = cinesNoSeleccionados;
            respuesta.Actores = pelicula.Actores;
            return respuesta;
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] PeliculasCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = await context.Peliculas
                .Include(p => p.PeliculasActores)
                .Include(p => p.PeliculasCines)
                .Include(p => p.PeliculasGeneros)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pelicula is null)
            {
                return NotFound();
            }
            pelicula = mapper.Map(peliculaCreacionDTO, pelicula);   
            if (peliculaCreacionDTO.Poster != null)
            {
                pelicula.Poster = await almacenadorArchivos.Editar( pelicula.Poster, contenedor,peliculaCreacionDTO.Poster);
            }
            AsignarOrdenActores(pelicula);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Pelicula>(id);
        }
    }
}
