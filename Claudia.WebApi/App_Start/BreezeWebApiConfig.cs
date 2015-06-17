using System.Web.Http;

//[assembly: WebActivator.PreApplicationStartMethod(
//    typeof(Claudia.WebApi.BreezeWebApiConfig), "RegisterBreezePreStart")]
namespace Claudia.WebApi {
  ///<summary>
  /// Inserts the Breeze Web API controller route at the front of all Web API routes
  ///</summary>
  ///<remarks>
  /// This class is discovered and run during startup; see
  /// http://blogs.msdn.com/b/davidebb/archive/2010/10/11/light-up-your-nupacks-with-startup-code-and-webactivator.aspx
  ///</remarks>
  /// +++++++MOVED TO WEBAPI CONFIG+++++++
  //public static class BreezeWebApiConfig {

  //  public static void RegisterBreezePreStart() {
  //    GlobalConfiguration.Configuration.Routes.MapHttpRoute(
  //        name: "BApi",
  //        routeTemplate: "bapi/{controller}/{action}"
  //    );
  //  }
  //}
}