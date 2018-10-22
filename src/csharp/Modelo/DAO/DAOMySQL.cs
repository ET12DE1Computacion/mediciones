using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Modelo;

namespace DAO
{
    public class DAOMySQL: IDAO
    {
        MySqlConnection conexion;
        MySqlCommand comando;
        MySqlDataAdapter adaptador;
        public void guardarMediciones(List<Medicion> mediciones)
        {
            mediciones.ForEach(m => this.guardarMediciones(m));
        }

        public void guardarTipoMedicion(TipoMedicion tipoMedicion)
        {
            throw new NotImplementedException();
        }
        public List<TipoMedicion> traerTipoMediciones()
        {
            throw new NotImplementedException();
        }

        private void guardarMediciones(Medicion medicion)
        {
            throw new NotImplementedException();
        }
    }
}
