using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitradMovil
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Sitrad",
                "sitrad/detalle/{IDTramite}/{Documento}",
                new { controller = "Sitrad", action = "Detalle", IDramite = 0, Documento = "" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Sitrad", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
