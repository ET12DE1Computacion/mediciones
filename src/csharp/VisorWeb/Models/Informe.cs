using System;
using System.Collections.Generic;

namespace VisorWeb.Models
{
    public partial class Informe
    {
        public int IdInforme { get; set; }
        public byte IdTipoMedicion { get; set; }
        public DateTime Fecha { get; set; }
        public float Minimo { get; set; }
        public float Maximo { get; set; }
        public float? Promedio { get; set; }

        public virtual Tipomedicion IdTipoMedicionNavigation { get; set; }
    }
}
