using System;
using System.Collections.Generic;

namespace Modelo.Formateadores
{
    public interface Formateador
    {
        List<Medicion> obtenerMedicionesDesde(Object paramGenerico, Logica logica);
    }
}
