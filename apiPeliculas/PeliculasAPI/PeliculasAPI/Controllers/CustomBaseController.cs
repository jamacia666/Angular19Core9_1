using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Utilidades;
using System.Linq.Expressions;

namespace PeliculasAPI.Controllers
{
    public class CustomBaseController : Controller
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly string cacheTag;

        public CustomBaseController(AplicationDbContext context, IMapper mapper, IOutputCacheStore outputCacheStore,
            string cacheTag
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.cacheTag = cacheTag;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacion,
            Expression<Func<TEntidad, object>> ordenarPor)
            where TEntidad : class
        {
            var queryable = context.Set<TEntidad>().AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            return await queryable
                .OrderBy(ordenarPor)
                .Paginar(paginacion)
                .ProjectTo<TDTO>(mapper.ConfigurationProvider).ToListAsync();
        }
        //         protected async Task<ActionResult> Post<TEntidad, TDTO>(TDTO dto) where TEntidad : class
        //        {
        //            var entidad = mapper.Map<TEntidad>(dto);
        //            context.Add(entidad);
        //            await context.SaveChangesAsync();
        //            var dtoCreado = mapper.Map<TDTO>(entidad);
        //            return new CreatedAtRouteResult("obtener" + typeof(TEntidad).Name, new { id = entidad.Id }, dtoCreado);
        //        }

        protected async Task<ActionResult<TdTO>> Get<TEntidad, TdTO>(int d)
            where TEntidad : class, IId
            where TdTO : IId
        {
            var entidad = await context.Set<TEntidad>()
               .ProjectTo<TdTO>(mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.Id == d);
            if (entidad == null)
            {
                return NotFound();
            }
            return entidad;
        }



        protected async Task<ActionResult> Post<TCreacionDTO, TEntidad, TDTO>
                (TCreacionDTO creacionDTO, string nombreRuta)
                where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(creacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            var entidadDTO = mapper.Map<TDTO>(entidad);
            return CreatedAtRoute(nombreRuta, new { id = entidad.Id }, entidadDTO);
        }

        protected async Task<ActionResult> Put<TCreacionDTO, TEntidad>
            (int id, TCreacionDTO creacionDTO)
            where TEntidad : class, IId
        {
            var entidadExiste = await context.Set<TEntidad>().AnyAsync(g => g.Id == id);
            if (!entidadExiste)
            {
                return NotFound();
            }
            var entidad = mapper.Map<TEntidad>(creacionDTO);

            entidad.Id = id;

            context.Update(entidad);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }

        protected async Task<IActionResult> Delete<TEntidad>(int id)
            where TEntidad : class, IId
        {
            var registrosBorrados = await context.Set<TEntidad>().Where(g => g.Id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }
    }
}

