using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Modelo;
using System.Linq;


namespace TestModelo
{
    [TestClass]
    public class TestFormateadorJson
    {
        const string stringJson = @"[{""n"":""humedad"",""v"":66},{""n"":""temperatura"",""v"":22},{""n"":""sensacionTermica"",""v"":21.97867}]";
        JArray vector;
        [TestInitialize]
        public void Iniciar()
        {
            vector = JArray.Parse(stringJson);
        }

        [TestMethod]
        public void TestFormateador()
        {
            Assert.AreEqual(3, vector.Count);
            List<Medicion> items = vector.Select(x => new Medicion
            {
                Valor = Convert.ToSingle(x["v"]),
                TipoMedicion = new TipoMedicion(x["n"].ToString())
            }).ToList();
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("humedad", items[0].TipoMedicion.Nombre);
            Assert.AreEqual(66, items[0].Valor);
        }
    }
}
