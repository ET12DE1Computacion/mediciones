using System;

namespace Modelo
{
    public class Medicion
    {
        int? id;
        TipoMedicion tipoMedicion;
        private float valor;
        DateTime fechaHora;

        public int? Id
        {
            get { return id; }
            set { id = value; }
        }
        public TipoMedicion TipoMedicion
        {
            get { return tipoMedicion; }
            set { tipoMedicion = value; }
        }
        
        public DateTime FechaHora
        {
            get { return fechaHora; }
            set { fechaHora = value; }
        }
        public float Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public override string ToString()
        {
            return string.Format("ID: {0}\tNombre: {1}\tValor:{2}\tFecha: {3} {4}",
                Id, TipoMedicion.Nombre, Valor, FechaHora.ToShortDateString(), FechaHora.ToLongTimeString());
        }
    }
}