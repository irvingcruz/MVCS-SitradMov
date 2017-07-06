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
            List<BESitrad> datosResult = new BLSitrad().fn_SelectDocumentosTramite(oTramite.IDTramite);
            return View("Resultado",datosResult.ToList());

            Terminar:
            return View();
        }

        public ActionResult Detalle(int IDTramite, string Documento)
        {
            Documento = Documento.Replace("|", "/");
            ViewBag.Documento = Documento;
            List<BESitrad> datosDetalle = new BLSitrad().fnTrazabilidadSITRAD(IDTramite, Documento);
            return View(datosDetalle.ToList());
        }

        
    }
}