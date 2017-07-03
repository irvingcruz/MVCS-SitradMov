using BusinessEntity;
using BusinessLogic;
using SitradMovil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static SitradMovil.Models.StringCrypto;

namespace SitradMovil.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            string Usuario = User.Identity.Name;
            if (Usuario == null || Usuario.Length == 0)
            {
                BEUsuario oUsuario = new BEUsuario();
                ViewBag.ReturnUrl = ReturnUrl;
                return View(oUsuario);
            }
            else
            {
                if (ReturnUrl == null || ReturnUrl == "/") return RedirectToAction("Index", "Home");
                else return RedirectToAction("AccessDenied", "Account");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(BEUsuario oUsuario, string ReturnUrl = "")
        {
            oUsuario.Alerta = "";
            if (!ModelState.IsValid)
            {
                goto Terminar;
            }

            StringCrypto Clave = new StringCrypto(SymmProvEnum.DES);
            string PasswordEncriptado;
            PasswordEncriptado = Clave.Encrypting(oUsuario.Password, "keyLogin");
            oUsuario.Password = PasswordEncriptado;

            if (new BLSitrad().fnAutenticacion(oUsuario))
            {
                oUsuario.Recordarme = true;
                oUsuario.Datos = oUsuario.Nombres + "|" + oUsuario.Correo;
                FormsAuthentication.SetAuthCookie(oUsuario.Datos, oUsuario.Recordarme);

                //System.Web.HttpContext.Current.Session["Usuario"] = oUsuario;
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Sitrad");
                }
            }
            else { oUsuario.Alerta = "(*) Las credenciales son incorrectas..!";  }
            ModelState.Remove("Password");
            Terminar:            
            return View(oUsuario);
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult AccessDenied()
        {
            ViewBag.Mensaje = "Lo sentimos, usted no tiene los permisos adecuados...!";
            return View();
        }
    }
}