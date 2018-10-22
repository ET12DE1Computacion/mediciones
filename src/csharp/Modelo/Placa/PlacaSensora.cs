using Modelo.Placa;
using System.Threading;

namespace Modelo
{
    public abstract class PlacaSensora
    {
        string nombrePlaca;
        Logica logica;
        Thread  hilo;

        internal Logica Logica
        {
            get { return logica; }
            set { logica = value; }
        }
        
        public string NombrePlaca
        {
            get { return nombrePlaca; }
            set { nombrePlaca = value; }
        }

        public PlacaSensora()
        {
            hilo = new Thread(t=>leer());
        }

        public bool leyendoAsincronicamente
        {
            get
            {
                return !hilo.IsAlive;
            }
        }

        public void iniciarLecturaAsincronica()
        {
            logica.debug("iniciarLecturaAsincronica - entre al loop infinito");
            hilo.Start();
        }

        public void pararLecturas()
        {
            hilo.Abort();
            disposear();
        }

        private void leer()
        {
            setup();
            while (true)
            {
                try
                {
                    logica.recibirMediciones(leerEspecifico());
                }
                catch (ExcepcionLectura el)
                {
                    logica.debug(el.Message);
                }
            }
        }

        protected abstract void setup();
        protected abstract object leerEspecifico();
        protected abstract void disposear();
    }
}
