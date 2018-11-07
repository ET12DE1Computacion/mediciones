using System.Threading;

namespace Modelo.Placa
{
    public class PlacaSimulada: PlacaSensora
    {
        const string stringJson = @"[{""n"":""Humedad"",""v"":66},{""n"":""Temperatura"",""v"":22},{""n"":""Sensacion Terminca"",""v"":21.97867}]";
        /// <summary>
        /// Propiedad que representa el tiempo en milisegundos que tarda en generar la lectura
        /// </summary>
        public int Delay { get; set; }
        public PlacaSimulada(): base() { }

        protected override void setup()
        {
            
        }

        protected override object leerEspecifico()
        {
            Thread.Sleep(Delay);
            return stringJson;
        }

        protected override void disposear()
        {
            
        }
    }
}