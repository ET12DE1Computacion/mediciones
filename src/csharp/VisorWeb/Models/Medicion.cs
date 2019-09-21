using System;
using System.Collections.Generic;

namespace VisorWeb.Models
{
    public partial class Medicion
    {
        public int IdMedicion { get; set; }
        public byte IdTipoMedicion { get; set; }
        public float Valor { get; set; }
        public DateTime FechaHora { get; set; }

        public virtual Tipomedicion IdTipoMedicionNavigation { get; set; }
    }
}
