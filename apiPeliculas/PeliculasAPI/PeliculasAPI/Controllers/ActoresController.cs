using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;
using PeliculasAPI.Utilidades;

namespace PeliculasAPI.Controllers
{

    [Route("api/actores")]
    [ApiController]
    public class ActoresController  :CustomBaseController
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private const string cacheTag = "actores";
        private const string contenedor = "actores";

        public ActoresController(AplicationDbContext context, IMapper mapper,
                IOutputCacheStore outputCacheStore, IAlmacenadorArchivos almacenadorArchivos)
        :base(context, mapper,outputCacheStore,cacheTag)  
    {
            this.context = context;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        [OutputCache(PolicyName = "cacheTag")]
        public async Task<List<ActorDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
        return await Get<Actor, ActorDTO>(paginacion, ordenarPor: g => g.Nombre);
             
    }


        [HttpGet("{id:int}", Name = "ObtenerActorPorId")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
           return await Get<Actor, ActorDTO>(id);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.foto != null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, actorCreacionDTO.foto);
                actor.Foto = url;
            }
            context.Add(actor);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return CreatedAtRoute("ObtenerActorPorId", new { id = actor.Id }, actor);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {

            var actor = await context.Actores.FirstOrDefaultAsync(a => a.Id == id);
            if (actor is null)
            {
                return NotFound();
            }
            actor = mapper.Map(actorCreacionDTO, actor);
            if (actorCreacionDTO.foto is not null)
            {
                actor.Foto = await almacenadorArchivos.Editar(actor.Foto, contenedor,
                    actorCreacionDTO.foto);
            }
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Actor>(id);

        }
    }
}

