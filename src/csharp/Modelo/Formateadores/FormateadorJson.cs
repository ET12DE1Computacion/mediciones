using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace Modelo.Formateadores
{
    public class FormateadorJson : Formateador
    {
        public List<Medicion> obtenerMedicionesDesde(object paramGenerico, Logica logica)
        {
            try
            {
                string cadenaJson = paramGenerico.ToString();
                JArray vectorJson = JArray.Parse(cadenaJson);
                DateTime ahora = DateTime.Now;
                List<Medicion> mediciones = vectorJson.Select(m=>new Medicion{
                    Valor = Convert.ToSingle(m["v"]),
                    TipoMedicion = logica.getTipoMedicion(m["n"].ToString()),
                    FechaHora = ahora}
                ).ToList();
                return mediciones;
            }
            catch (Newtonsoft.Json.JsonException e)
            {
                throw new ExcepcionFormato("Error de formato Json: " + e.Message);
            }
        }
    }
}
