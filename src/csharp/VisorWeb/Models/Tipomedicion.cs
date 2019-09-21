using System;
using System.Collections.Generic;

namespace VisorWeb.Models
{
    public partial class Tipomedicion
    {
        public Tipomedicion()
        {
            Informe = new HashSet<Informe>();
            Medicion = new HashSet<Medicion>();
        }

        public byte IdTipoMedicion { get; set; }
        public string TipoMedicion1 { get; set; }

        public virtual ICollection<Informe> Informe { get; set; }
        public virtual ICollection<Medicion> Medicion { get; set; }
    }
}
