using RateListing.Ui.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace RateListing.Ui
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("disallow", typeof(DisallowListConstraint));
            routes.MapMvcAttributeRoutes(constraintsResolver);

            routes.MapRoute(
               name: "UrlRewrite",
               url: "{firmaUrl}",
               defaults: new { controller = "Companies", action = "Details" },
               namespaces: new string[] { "RateListing.Ui.Controllers" }

               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "RateListing.Ui.Controllers" }
            );
        }
    }
}
