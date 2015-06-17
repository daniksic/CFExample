using System.Web;
using System.Web.Optimization;

namespace Claudia.Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/claudia.js",
                      "~/Scripts/pinit.js"));

            bundles.Add(new ScriptBundle("~/bundles/googleAnalytics").Include(
                      "~/Scripts/googleAnalytics.js"));

            bundles.Add(new ScriptBundle("~/bundles/galleria").Include(
                      "~/Content/galleria/galleria-1.3.5.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-united.css",
                      "~/Content/myCSS.css",
                      "~/Content/galleria/themes/classic/galleria.classic.css",
                      "~/Content/font-awesome.css"));


            bundles.Add(new ScriptBundle("~/cms/imagecroptool").Include(
                "~/scripts/ImageCropTool.js"
                ));

            #region CMS

            bundles.Add(new ScriptBundle("~/cms/vendor").Include(
                "~/scripts/jquery-2.1.0.js",
                "~/scripts/angular.js",
                "~/scripts/angular-animate.js",
                "~/scripts/angular-route.js",
                //"~/scripts/angular-sanitize.js",
                "~/scripts/bootstrap.js",
                "~/scripts/toastr.js",
                "~/scripts/moment.js",
                "~/scripts/ui-bootstrap-tpls-0.10.0.js",
                "~/scripts/spin.js",
                "~/scripts/angular-xeditable.js",
                "~/scripts/textAngular.js",
                "~/scripts/textAngular-sanitize.js", //not use with angular-sanit, one or other!!!
                "~/scripts/angular-file-upload.js",
                "~/scripts/breeze.debug.js",
                "~/scripts/breeze.angular.js",
                "~/scripts/breeze.directives.js",
                "~/scripts/breeze.saveErrorExtensions.js",
                "~/scripts/ImageCropTool.js"
                ));

            bundles.Add(new ScriptBundle("~/cms/app")
                .IncludeDirectory("~/app", "*.js", false)
                .IncludeDirectory("~/app/common", "*.js", true)
                .IncludeDirectory("~/app/services", "*.js", true)
                );
                //.Include(
                //"~/app/app.js",
                //"~/app/config.js",
                //"~/app/config.exceptionHandler.js",
                //"~/app/config.route.js",
                //"~/app/common/common.js",
                //"~/app/common/logger.js",
                //"~/app/common/spinner.js",
                //"~/app/common/bootstrap/bootstrap.dialog.js",
                //"~/app/services/datacontext.js",
                //"~/app/services/directives.js",
                //"~/app/services/entityManagerFactory.js",
                //"~/app/services/routemediator.js",
                //"~/app/services/models.js"
                //));

            bundles.Add(new ScriptBundle("~/cms/view")
                .IncludeDirectory("~/app/layout", "*.js", true)
                .IncludeDirectory("~/app/admin", "*.js", true)
                .IncludeDirectory("~/app/dashboard", "*.js", true)
                .IncludeDirectory("~/app/youtube", "*.js", true)
                .IncludeDirectory("~/app/gallery", "*.js", true)
                .IncludeDirectory("~/app/carousel", "*.js", true)
                .IncludeDirectory("~/app/comments", "*.js", true)
                .IncludeDirectory("~/app/recipe", "*.js", true)
                );
                //.Include(
                //"~/app/layout/shell.js",
                //"~/app/layout/sidebar.js",
                //"~/app/admin/admin.js",
                //"~/app/dashboard/dashboard.js",
                //"~/app/youtube/youtube.js"
                //));

            bundles.Add(new StyleBundle("~/cms/css").Include(
                "~/content/ie10mobile.css",
                "~/content/bootstrap.min.css",
                "~/content/font-awesome.min.css",
                "~/content/toastr.css",
                "~/content/customtheme.css",
                "~/content/styles.css",
                "~/content/angular-xeditable.css",
                "~/content/breeze.directives.css"
                ));

            #endregion
        }
    }
}
