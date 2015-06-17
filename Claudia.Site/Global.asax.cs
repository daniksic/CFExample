using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Claudia.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.Init();

            //CacheData.LoadIntoCache();

            //Claudia.Domain.EventManager.EventManager.Instance.Subscribe(new Claudia.Site.Helpers.QuickData(), "breeze.savedlinktodb",
            //    () => { System.Diagnostics.Debug.WriteLine("bik te jebi"); }
            //    );
        }


    }
}
