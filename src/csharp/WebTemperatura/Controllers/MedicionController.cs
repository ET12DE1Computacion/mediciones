﻿using System;
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
            medicionesEntities context = new medicionesEntities();
            context.Configuration.ProxyCreationEnabled = false;
            var med = context.medicion.Where(x => x.fechaHora > DateTime.Now && x.idTipoMedicion == 1).OrderBy(x => x.fechaHora).First();
            return Json(new { Response = med }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUltimaMedicionTabla()
        {
            medicionesEntities context = new medicionesEntities();
            context.Configuration.ProxyCreationEnabled = false;
            var med = context.medicion.Where(x => x.fechaHora > DateTime.Now).OrderBy(x => x.fechaHora).First();
            return Json(new { Response = med }, JsonRequestBehavior.AllowGet);
        }
    }
}