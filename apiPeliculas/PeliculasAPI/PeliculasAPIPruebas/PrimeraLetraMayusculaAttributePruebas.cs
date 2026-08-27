using PeliculasAPI.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeliculasAPIPruebas
{
    [TestClass]
    public sealed class PrimeraLetraMayusculaAttributePruebas
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("     ")]
        [DataRow(null)]
        public void IsValid_DebeRetornarExitoso_SiElValorEsVacio(string valor)
        {
            // Preparar 
            var primeraLetraMayusculaAtribute = new PrimeraLetraMayúsculaAtribute();
            var validacionContext = new ValidationContext(new object());            
            // Probar
            var resultado = primeraLetraMayusculaAtribute.GetValidationResult(valor, validacionContext);
            // Verificar
            Assert.AreEqual(expected: ValidationResult.Success,actual: resultado);
        }
        [TestMethod]
        [DataRow("felipe")]
        public void IsValid_DebeRetornarError_SiLaPrimeraLetraNoEsMayuscula(string valor)
        {
            // Preparar
            var primeraLetraMayusculaAttribute = new PrimeraLetraMayúsculaAtribute();
            var validationContext = new ValidationContext(new object());

            // Probar
            var resultado = primeraLetraMayusculaAttribute.GetValidationResult(valor, validationContext);

            // Verificar
            Assert.AreEqual(expected: "La primera letra debe ser mayúscula",
                actual: resultado!.ErrorMessage);
        }

    }
}
