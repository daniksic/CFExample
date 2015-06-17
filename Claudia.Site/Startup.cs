using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Claudia.Site.Startup))]
namespace Claudia.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseWebApi(Claudia.WebApi.WebApiConfig.GetConfig());

            
        }
    }
}
