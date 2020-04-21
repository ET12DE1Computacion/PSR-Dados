using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dados.Core
{
    public class Simulacion
    {
        public DateTime FechaHora { get; private set; }

        /// <summary>
        /// Propiedad que devuelve el intervalo de tiempo desde que se reseteo el cronómetro
        /// </summary>
        public TimeSpan Duracion => DateTime.Now - FechaHora;
        public Simulacion()
        {
            ResetearCronometro();
        }
        public void ResetearCronometro() => FechaHora = DateTime.Now;
        public int SimularSinHilos(Dado dado, byte numero, int cantidadSimulaciones)
            => dado.TirarNVeces(numero, cantidadSimulaciones);
        public int SimularConHilos(Dado dado, byte numero, int cantidadSimulaciones, int cantidadHilos)
        {
            Task<int>[] tareas = new Task<int>[cantidadHilos];
            int simulacionesPorHilo = cantidadSimulaciones / cantidadHilos;
            
            //Instanciamos cada hilo, clonando un dado por cada hilo                     
            for (int i = 0; i < cantidadHilos; i++)
            {
                Dado clon = (Dado)dado.Clone();                
                tareas[i] = new Task<int>(()=>clon.TirarNVeces(numero, simulacionesPorHilo));
            }

            //Ejecutamos los hilos que instanciamos
            Array.ForEach(tareas, t => t.Start());

            //Esperamos que todos terminen su ejecución
            Task<int>.WaitAll(tareas);

            return tareas.Sum(t => t.Result);
        }
    }
}