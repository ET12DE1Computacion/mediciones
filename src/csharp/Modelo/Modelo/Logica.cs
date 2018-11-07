using DAO;
using Modelo.Formateadores;
using Modelo.Placa;
using System.Collections.Generic;

namespace Modelo
{
    public class Logica
    {
        IDisplay visor;
        IDAO dao;

        public IDAO Dao
        {
            get { return dao; }
            set { dao = value; }
        }
        public Formateador Formateador { get; set; }
        public Dictionary<string,TipoMedicion> TiposMediciones { get; set; }
        private List<PlacaSensora> placas = new List<PlacaSensora>();
        public IDisplay Visor
        {
            get { return visor; }
            set { visor = value; }
        }
        public Logica()
        {
            TiposMediciones = new Dictionary<string, TipoMedicion>();
        }

        public Logica(IDAO dao)
        {
            this.TiposMediciones = new Dictionary<string, TipoMedicion>();
            this.Dao = dao;
            this.agregarTipoMedicion(dao.traerTipoMediciones());
        }
        /// <summary>
        /// Metodo que devuelve el tipo de medicion, asociado al nombre del parametro.
        /// En caso de no poseer la medicion, la da de alta, la persiste y devuelve.
        /// </summary>
        /// <param name="nombre">Nombre del Tipo que se espera</param>
        /// <returns>Tipo asociado al nombre</returns>
        public TipoMedicion getTipoMedicion(string nombre)
        {
            TipoMedicion tipoMedicion;
            if (TiposMediciones.ContainsKey(nombre))
            {
                tipoMedicion = TiposMediciones[nombre];
            }
            else
            {
                tipoMedicion = new TipoMedicion(nombre);
                dao.guardarTipoMedicion(tipoMedicion);
                this.agregarTipoMedicion(tipoMedicion);            
            }
            return tipoMedicion;           
        }       

        internal void recibirMediciones(object medicionesBoxeadas)
        {
            try
            {
                List<Medicion> mediciones = Formateador.obtenerMedicionesDesde(medicionesBoxeadas, this);
                dao.guardarMediciones(mediciones);
                visor.imprimirMediciones(mediciones);
            }
            catch (ExcepcionLectura e)
            {
                debug(e.Message);
            }
            catch (ExcepcionFormato e)
            {
                debug(e.Message);
            }
        }      
        public void EliminarPlaca( PlacaSensora unaPlaca)
        {
            placas.Remove(unaPlaca);
        }
        public void AgregarPlaca(PlacaSensora unaPlaca)
        {
            placas.Add(unaPlaca);
            unaPlaca.Logica = this;
            unaPlaca.iniciarLecturaAsincronica();
            debug("sali del setter");
        }

        public void debug(string debug)
        {
            Visor.debug(debug);
        }

        public void agregarTipoMedicion(TipoMedicion tipoMedicion)
        {
            TiposMediciones.Add(tipoMedicion.Nombre, tipoMedicion);
        }

        public void agregarTipoMedicion(List<TipoMedicion> tiposMediciones)
        {
            tiposMediciones.ForEach(tm => this.agregarTipoMedicion(tm));
        }
    }
}
