using Dados.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dados.Test.PruebaSimple
{
    [TestClass]
    public class SimulacionDadoUnaCara
    {
        public Simulacion Simulacion { get; set; }
        public Dado Dado { get; set; }

        [TestInitialize]
        public void Iniciar()
        {
            Simulacion = new Simulacion();
            Dado = new Dado(1);
        }

        [TestMethod]
        public void GanarNVeces()
        {
            // Dependiento de la computadora que tengan, este test puede llevar cierto tiempo.
            int cantidadSimulaciones = 20000000;
            int cantidadHilos = 4;

            Simulacion.ResetearCronometro();
            var cantidadSinHilos = Simulacion.SimularSinHilos(Dado, 1, cantidadSimulaciones);
            var duracionSinHilos = Simulacion.Duracion;

            Simulacion.ResetearCronometro();
            var cantidadConHilos = Simulacion.SimularConHilos(Dado, 1, cantidadSimulaciones, cantidadHilos);
            var duracionConHilos = Simulacion.Duracion;

            Assert.AreEqual(cantidadSimulaciones, cantidadSinHilos);
            Assert.AreEqual(cantidadSimulaciones, cantidadConHilos);

            Assert.IsTrue(duracionConHilos < duracionSinHilos);
        }
    }
}