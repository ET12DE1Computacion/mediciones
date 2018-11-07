using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Placa;
using Modelo.Formateadores;
using Modelo;
using DAO;

namespace VisorConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            VisorConsola visor = new VisorConsola();

            PlacaSensora placa = new Serial(9600, "COM5");
            placa.NombrePlaca = "Arduino Uno - DHT11";

            PlacaSensora placa2 = new PlacaSimulada
            {
                NombrePlaca = "Simulada",
                Delay = 2500
            };

            PlacaSensora placa3 = new PlacaSimulada{
                NombrePlaca = "Simulada1",
                Delay = 1300
            };

            PlacaSensora placa4 = new PlacaSimulada{
                    NombrePlaca = "Simulada2",
                    Delay = 7500
            };
            
            FormateadorJson formateador = new FormateadorJson();

            DAOMySQL dao = new DAOMySQL("server=localhost;database=mediciones;uid=mediciones;pwd=mediciones;");

            Logica logica = new Logica(dao);
            logica.Visor = visor;
            logica.Formateador = formateador;
           //( logica.AgregarPlaca(placa);
            logica.AgregarPlaca(placa2);
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
