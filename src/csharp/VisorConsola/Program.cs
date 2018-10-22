using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Placa;
using Modelo.Formateadores;
using Modelo;

namespace VisorConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            VisorConsola visor = new VisorConsola();

            Modelo.PlacaSensora placa = new Serial(9600, "COM5");
            placa.NombrePlaca = "Arduino Uno - DHT11";

            FormateadorJson formateador = new FormateadorJson();

            List<TipoMedicion> tiposMediciones = new List<TipoMedicion>{
                new TipoMedicion("humedad"),
                new TipoMedicion("temperatura"),
                new TipoMedicion("sensacionTermica")
            };

            Logica logica = new Logica();
            logica.agregarTipoMedicion(tiposMediciones);
            logica.Visor = visor;
            logica.Formateador = formateador;
            logica.Placa = placa;
            while (true) ;
        }
    }

    class VisorConsola : IDisplay
    {

        public void imprimirMediciones(List<Medicion> mediciones)
        {
            mediciones.ForEach(m => Console.WriteLine(m));
            Console.WriteLine();
        }
        public void debug(string debug)
        {
            Console.WriteLine(debug);
        }
    }
}
