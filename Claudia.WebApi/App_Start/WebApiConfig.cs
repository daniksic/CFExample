using System.Web.Http;

namespace Claudia.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static HttpConfiguration GetConfig()
        {
            var cfg = new HttpConfiguration();

            cfg.MapHttpAttributeRoutes();

            cfg.Routes.MapHttpRoute(
                name: "BApi",
                routeTemplate: "bapi/{action}",
                defaults: new { controller = "Breeze" }
                );

            cfg.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            return cfg;
        }
    }
}
