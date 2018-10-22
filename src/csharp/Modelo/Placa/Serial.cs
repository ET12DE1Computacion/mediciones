using System;
using System.IO;
using System.IO.Ports;

namespace Modelo.Placa
{
    public class Serial: PlacaSensora
    {
        int baudios;
        string nombrePuerto;
        SerialPort puerto;

        public Serial(int baudios, string nombrePuerto): base()
        {
            this.baudios = baudios;
            this.nombrePuerto = nombrePuerto;
            puerto = new SerialPort(nombrePuerto, baudios);
        }
        protected override object leerEspecifico()
        {
            try
            {
                if (!puerto.IsOpen)
                {
                    puerto.Open();
                }
                return puerto.ReadLine();
            }
            catch (IOException e)
            {
                throw new ExcepcionLectura();
            }
            catch (InvalidOperationException e)
            {
                throw new ExcepcionLectura("Puerto Desconectado");
            }
        }

        protected override void setup()
        {
            do
            {
                try
                {
                    puerto.Open();
                }
                catch (Exception e)
                {
                    Logica.debug("Puerto no se pudo abrir: " + e.Message);
                }
            } while (!puerto.IsOpen);
        }

        protected override void disposear()
        {
            puerto.Close();
        }
    }
}