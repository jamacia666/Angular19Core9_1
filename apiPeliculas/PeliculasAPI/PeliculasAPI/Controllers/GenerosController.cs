using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Utilidades;
using System.Text;
using System.Threading.Tasks;






namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {  
        private readonly IOutputCacheStore _outputCacheStore;
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private const string cacheTag="generos";

        public GenerosController( IOutputCacheStore outputCacheStore,
           AplicationDbContext context,
           IMapper mapper) 

        {    
            _outputCacheStore = outputCacheStore;
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet] //api/generos
        public async Task<List<GeneroDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            //var generos = await context.Generos.ToListAsync();
            //var generosDTOs = mapper.Map<List<GeneroDTO>>(generos);
            //return generosDTOs; 
            var queryable = context.Generos;
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            return  await queryable
                .OrderBy(g => g.Nombre)
                .Paginar(paginacion) 
                .ProjectTo<GeneroDTO>(mapper.ConfigurationProvider).ToListAsync();
        }
        
        [HttpGet("{id:int}", Name="ObtenerGeneroPorId")]  //api/generos/500
        [OutputCache(Tags =[cacheTag])]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
           var genero = await context.Generos
                .ProjectTo<GeneroDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (genero == null)
            {
                return NotFound();
            }
            
            return genero;       
        }

        [HttpGet("{nombre}")] //api/generos/Felipe
        public async Task<Genero?> Get(string nombre)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            await _outputCacheStore.EvictByTagAsync(cacheTag,default);
            return Ok(genero);
           // return CreatedAtRoute("ObtenerGeneroPorId", new { id = genero.Id }, genero);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var generoExiste = await context.Generos.AnyAsync(g => g.Id == id);
            if (!generoExiste)
            {
                return NotFound();
            }
            var genero = mapper.Map<Genero>(generoCreacionDTO);

            genero.Id = id;

            context.Update(genero);
            await context.SaveChangesAsync();
            await _outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }
        [HttpDelete ("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
           var registrosBorrados = await context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }   
             await _outputCacheStore.EvictByTagAsync(cacheTag, default);
             return NoContent();    

        }
    }
}