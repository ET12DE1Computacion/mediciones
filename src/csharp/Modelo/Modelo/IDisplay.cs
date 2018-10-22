using System.Collections.Generic;

namespace Modelo
{
    public interface IDisplay
    {
        void imprimirMediciones (List<Medicion> mediciones);
        void debug(string debug);
    }
}
