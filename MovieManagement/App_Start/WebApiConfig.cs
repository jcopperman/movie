using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MovieManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API custom routes
            config.MapHttpAttributeRoutes();

            // Conventional routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
