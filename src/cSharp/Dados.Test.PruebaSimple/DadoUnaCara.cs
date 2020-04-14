using Dados.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dados.Test.PruebaSimple
{
    [TestClass]
    public class DadoUnaCara
    {
        public Dado Dado { get; set; }

        [TestInitialize]
        public void Iniciar()
        {
            Dado = new Dado(1);
        }

        [TestMethod]
        public void Sale1()
        {
            Dado.Tirar();
            Assert.IsTrue(Dado.Salio(1));
            Assert.IsFalse(Dado.Salio(7));
        }

        [TestMethod]
        public void GanarNVeces()
        {
            int cantidad = 1000;

            var resultado = Dado.TirarNVeces(1, cantidad);
            Assert.AreEqual(cantidad, resultado);
        }
    }
}