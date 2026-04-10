using Microsoft.AspNetCore.StaticAssets;
using PeliculasAPI.DTOs;
using System.Runtime.CompilerServices;

namespace PeliculasAPI.Utilidades
{
    public static class IQueryableExtensions
    {
        public static IQueryable<I>Paginar<I>(this IQueryable<I> queryable, PaginacionDTO paginacion)
        {
            return queryable
                  .Skip((paginacion.Pagina - 1) * paginacion.RecordsPorPagina)
                  .Take(paginacion.RecordsPorPagina);
        }        

        

    }
}
