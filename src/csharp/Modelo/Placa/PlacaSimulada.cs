﻿using System.Threading;

namespace Modelo.Placa
{
    public class PlacaSimulada: PlacaSensora
    {
        const string stringJson = @"[{""n"":""humedad"",""v"":66},{""n"":""temperatura"",""v"":22},{""n"":""sensacionTermica"",""v"":21.97867}]";
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