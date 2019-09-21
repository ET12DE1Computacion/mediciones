using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Placa;
using Modelo.Formateadores;
using Modelo;
using DAO;
using System.Configuration;

namespace VisorConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            VisorConsola visor = new VisorConsola();

            PlacaSensora placaFisica = new Serial(9600, "COM7");
            placaFisica.NombrePlaca = "Arduino Uno - DHT11";

            PlacaSensora placa2 = new PlacaSimulada{
                NombrePlaca = "Simulada",
                Delay = 2500
            };
            FormateadorJson formateador = new FormateadorJson();

            var connectionString = string.Format("server={0};database={1};uid={2};pwd={3};",
                ConfigurationManager.AppSettings["server"], 
                ConfigurationManager.AppSettings["database"], 
                ConfigurationManager.AppSettings["user"], 
                ConfigurationManager.AppSettings["password"]);

            //DAOMySQL dao = new DAOMySQL("server=localhost;database=mediciones;uid=root;pwd=telesca1234;");
            DAOMySQL dao = new DAOMySQL(connectionString);


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Logica logica = new Logica(dao);
            logica.Visor = visor;
            logica.Formateador = formateador;
            logica.Placa = placaFisica;
            while (true) ;
        }
    }

    class VisorConsola : IDisplay
    {

        public void imprimirMediciones(List<Medicion> mediciones)
        {
            imprimirLinea();
            mediciones.ForEach(m => imprimirMedicion(m));
            imprimirLinea();
        }
        public void debug(string debug)
        {
            Console.WriteLine(debug);
        }

        private void imprimirMedicion(Medicion m)
        {
            string cad = string.Format("{0} = {1}\t{2}", m.TipoMedicion.Nombre, m.Valor, m.FechaHora);
            Console.WriteLine(cad);
        }

        private void imprimirLinea()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }
    }
}
