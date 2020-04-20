using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dados.Core
{
    public class Simulacion
    {
        public int SimularSinHilos(Dado dado, byte numero, int cantidadSimulaciones)
            => dado.TirarNVeces(numero, cantidadSimulaciones);
        public int SimularConHilos(Dado dado, byte numero, int cantidadSimulaciones, int cantidadHilos)
        {
            Task<int>[] tareas = new Task<int>[cantidadHilos];
            int simulacionesPorHilo = cantidadSimulaciones / cantidadHilos;
            
            //Instanciamos cada hilo, clonando el dado                     
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