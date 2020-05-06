using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TaskManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{user}/{project}/{type}",
                defaults: new { controller = "ProjectManager", action = "Index", user = UrlParameter.Optional, project = UrlParameter.Optional, type = UrlParameter.Optional }
            );
        }
    }
}
