﻿using System;
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

            PlacaSensora placaFisica = new Serial(9600, "COM3");
            placaFisica.NombrePlaca = "Arduino Uno - DHT11";

            PlacaSensora placa2 = new PlacaSimulada{
                NombrePlaca = "Simulada",
                Delay = 2500
            };
            FormateadorJson formateador = new FormateadorJson();

            DAOMySQL dao = new DAOMySQL("server=localhost;database=mediciones;uid=root;pwd=admin;");


            Console.BackgroundColor = ConsoleColor.DarkGreen;
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
            //string cad = $"{m.TipoMedicion.ToString()} = {m.Valor}\t{m.FechaHora.ToString()}";
            string cad = string.Format("{0} = {1} \t {2}", m.TipoMedicion, m.FechaHora.ToString());
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
