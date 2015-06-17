using Nancy;
using Nancy.Diagnostics;

namespace Claudia.Web.Nancyfx
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {

        //protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        //{
        //    base.ApplicationStartup(container, pipelines);

            ////Adding custom view resolver route
            //this.Conventions.ViewLocationConventions.Add((viewName, model, context)=>
            //    {
            //        return string.Concat("../Claudia.Site/Views/CMS/", viewName);
            //    });
        //}

        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get
            {
                //return base.DiagnosticsConfiguration;
                return new DiagnosticsConfiguration { Password = @"ktb" };
            }
        }
    }
}