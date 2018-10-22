using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Modelo;

namespace DAO
{
    public class DAOMySQL: IDAO
    {
        public void guardarMediciones(List<Medicion> mediciones)
        {
            throw new NotImplementedException();
        }

        public void guardarTipoMedicion(TipoMedicion tipoMedicion)
        {
            throw new NotImplementedException();
        }
        public List<TipoMedicion> traerTipoMediciones()
        {
            throw new NotImplementedException();
        }
    }
}
