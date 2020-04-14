using System;

namespace Dados.Core
{
    public class Dado
    {
        public byte CaraSuperior { get; private set; }
        public byte CantidadCaras { get; private set; }
        private Random Azar { get; set; }
        public Dado(byte cantidadCaras)
        {
            CantidadCaras = cantidadCaras;
            Azar = new Random(DateTime.Now.Millisecond);
        }
        public bool Salio(byte numero) => CaraSuperior == numero;
        public void Tirar()
            => CaraSuperior = Convert.ToByte(Azar.Next(CantidadCaras) + 1);
        public int TirarNVeces(byte numero, int cantidadVeces)
        {
            int contador = 0;
            for (int i = 0; i < cantidadVeces; i++)
            {
                Tirar();
                if (Salio(numero))
                {
                    contador++;
                }
            }
            return contador;
        }
    }
}