using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTemperatura.Models;

namespace WebTemperatura.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ReturnLast(){
            medicionesEntities context = new medicionesEntities();
            medicion m = new medicion();
                context.medicion.Where(x => x.fechaHora > DateTime.Now).OrderBy(x => x.fechaHora).First();         

            return null;
        }
    }
}