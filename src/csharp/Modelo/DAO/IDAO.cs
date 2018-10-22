using Modelo;
using System.Collections.Generic;

namespace DAO
{
    public interface IDAO
    {
        void guardarMediciones(List<Medicion> mediciones);
        void guardarTipoMedicion(TipoMedicion tipoMedicion);
        List<TipoMedicion> traerTipoMediciones();
    }
}
