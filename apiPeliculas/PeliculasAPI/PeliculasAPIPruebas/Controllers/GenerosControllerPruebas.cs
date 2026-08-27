using System;
using System.Collections.Generic;
using System.Text;



using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;


namespace PeliculasAPIPruebas.Controllers
{
    [TestClass]
    public sealed class GenerosControllerPruebas:BasePruebas
    {
        [TestMethod]
        public async Task Get_DevuelveTodosLosGeneros()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();
            IOutputCacheStore outputCacheStore = null!;

            contexto.Generos.Add(new Genero() { Nombre = "Género 1" });
            contexto.Generos.Add(new Genero() { Nombre = "Género 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new GenerosController(outputCacheStore, contexto2, mapper);

            // Prueba
            var respuesta = await controller.Get();

            // Verificación
            Assert.AreEqual(expected: 2, actual: respuesta.Count);
        }
    }
}
