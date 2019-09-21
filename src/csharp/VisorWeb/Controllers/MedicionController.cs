using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VisorWeb.Models;

namespace WebTemperatura.Controllers
{
    public class MedicionController : Controller
    {
        private readonly MedicionesContext context;

        public MedicionController(MedicionesContext context)
        {
            this.context = context;
        }

        // GET: Medicion
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grafico()
        {
            return View(); 
        }

        public IActionResult Tabla()
        {
            return View();
        }

        //[HttpGet]
        //public JsonResult GetUltimaMedicion()
        //{
        //    DateTime fechaReferencia = DateTime.Now.AddSeconds(-10);
        //    Medicion med = context.Medicion
        //        .FirstOrDefault(x => x.FechaHora > fechaReferencia);
        //    return Json( new { Response = new { idMedicion = med.IdMedicion, tipoMedicion = med.IdTipoMedicionNavigation.TipoMedicion1, fechaHora = med.FechaHora } } );
        //}

        [HttpGet]
        public JsonResult GetUltimaMedicionTemperatura()
        {
            DateTime fechaReferencia = DateTime.Now.AddSeconds(-3);
            Medicion med = context.Medicion.FirstOrDefault(x => x.FechaHora > fechaReferencia && x.IdTipoMedicionNavigation.TipoMedicion1 == "temperatura");
            return Json( new { idMedicion = med.IdMedicion, valor = med.Valor, fechaHora = med.FechaHora, tipoMedicion = "temperatura" } );
        }

        [HttpGet]
        public JsonResult GetUltimaMedicionSensacionTermica()
        {

            DateTime fechaReferencia = DateTime.Now.AddSeconds(-3);
            Medicion med = context.Medicion.FirstOrDefault(x => x.FechaHora > fechaReferencia && x.IdTipoMedicionNavigation.TipoMedicion1 == "sensacionTermica");
            return Json( new { idMedicion = med.IdMedicion, valor = med.Valor, fechaHora = med.FechaHora, tipoMedicion = "sensacionTermica" });
        }

        [HttpGet]
        public JsonResult GetUltimaMedicionHumedad()
        {
            DateTime fechaReferencia = DateTime.Now.AddSeconds(-3);
            Medicion med = context.Medicion.FirstOrDefault(x => x.FechaHora > fechaReferencia && x.IdTipoMedicionNavigation.TipoMedicion1 == "humedad");
            return Json( new { idMedicion = med.IdMedicion, valor = med.Valor, fechaHora = med.FechaHora, tipoMedicion = "humedad" });
        }
    }
}