using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using eeSea.Models;

namespace eeSea
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new SubdomainRoute());

            routes.MapRoute("Main", "{action}", new { controller = "Home", action = "Main" });
            //routes.MapRoute("Locations", "{action}", new { controller = "Locations", action = "Index" });
            //routes.MapRoute("DistanceAndLinks", "Locations/DistanceAndLinks", new { controller = "DistanceAndLinks", action = "DistanceAndLinks" });
            

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

          

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<EeSeaContext>(null);
        }
    }

    public class SubdomainRoute : RouteBase
    {

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var url = httpContext.Request.Headers["HOST"];
            var index = ".".IndexOf(url, System.StringComparison.Ordinal);

            if (index < 0)
                return null;

            var subDomain = url.Substring(0, index).ToLower();

            var routeData = new RouteData(this, new MvcRouteHandler());

            switch (subDomain)
            {
                case "locations":
                    routeData.Values.Add("controller", "LocationsController"); //Goes to the LocationsController class
                    routeData.Values.Add("action", "Index"); //Goes to the Index action on the LocationsController
                    break;
                case "vessels":
                    routeData.Values.Add("controller", "VesselsController"); //Goes to the VesselsController class
                    routeData.Values.Add("action", "Index"); //Goes to the Index action on the VesselsController
                    break;
                case "services":
                    routeData.Values.Add("controller", "ServicesController"); //Goes to the ServicesController class
                    routeData.Values.Add("action", "Index"); //Goes to the Index action on the ServicesController
                    break;
                default:
                    routeData.Values.Add("controller", "Home"); //Goes to the HomeController class
                    routeData.Values.Add("action", "Main"); //Goes to the Main action on the HomeController
                    break;
            }

            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }
    }


}