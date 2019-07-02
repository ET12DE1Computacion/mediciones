using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTemperatura.Models;

namespace WebTemperatura.Controllers
{
    public class MedicionController : Controller
    {
        // GET: Medicion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grafico()
        {
            return View(); 
        }

        public ActionResult Tabla()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUltimaMedicion()
        {
            Medicion med; 

            using (var context = new medicionesEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                DateTime fechaReferencia = DateTime.Now.AddSeconds(-10);
                med = context.Medicion.FirstOrDefault(x => x.fechaHora > fechaReferencia && x.idTipoMedicion == 3);
            }

            return Json(new { Response = med }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUltimaMedicionTabla()
        {
            Medicion med;
            using (var context = new medicionesEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                DateTime fechaReferencia = DateTime.Now.AddSeconds(-10);
                med = context.Medicion.FirstOrDefault(x => x.fechaHora > fechaReferencia && x.idTipoMedicion == 1);
            }
            return Json(new { Response = med }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUltimaMedicionTablaST()
        {
            Medicion med;

            using (var context = new medicionesEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                DateTime fechaReferencia = DateTime.Now.AddSeconds(-10);
                med = context.Medicion.FirstOrDefault(x => x.fechaHora > fechaReferencia && x.idTipoMedicion == 4);
            }

            return Json(new { Response = med }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUltimaMedicionTablaH()
        {
            Medicion med;
            using (var context = new medicionesEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                DateTime fechaReferencia = DateTime.Now.AddSeconds(-10);
                med = context.Medicion.FirstOrDefault(x => x.fechaHora > fechaReferencia && x.idTipoMedicion == 3);
            }
            return Json(new { Response = med }, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult Test()
        //{
        //    medicionesEntities1 context = new medicionesEntities1();
        //    context.Configuration.ProxyCreationEnabled = false;
        //    var med = context.medicion.ToList();
        //    return Json(new { Response = "" }, JsonRequestBehavior.AllowGet);
        //}
    }
}