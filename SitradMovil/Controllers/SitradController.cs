using BusinessEntity;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitradMovil.Controllers
{
    [Authorize]
    public class SitradController : Controller
    {
        public ActionResult Index()
        {
            BESitrad oTramite = new BESitrad();
            return View(oTramite);
        }
        public ActionResult Resultado() {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Resultado(BESitrad oTramite)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            string[] valores = oTramite.IDTramite.Split(Convert.ToChar("-"));
            if (valores.Length != 2)
            {
                goto Terminar;
            }
            ViewBag.Tramite = oTramite.IDTramite;
            List<BESitrad> datosResult = new BLSitrad().fnTrazabilidadSITRAD(oTramite.IDTramite);
            return View("Resultado",datosResult.ToList());

            Terminar:
            return View();
        }

        
    }
}