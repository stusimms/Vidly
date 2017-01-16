using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //method to allow attribute routing
            routes.MapMvcAttributeRoutes();

            //conventional way to create custom route rather than the new means of attribute routing
            //this example is for movies by release year and month
            //controlled by ByReleaseDate action within MoviesController
            //restricts year parameter to be a 4 digit number and month parameter to be a 2 digit number
            //routes.MapRoute(
                //"MoviesByReleaseDate",
                //"movies/released/{year}/{month}",
                //new {controller = "Movies", action = "ByReleaseDate"},
                //new {year = @"\d{4}",month =@"\d{2}"});

            //default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
