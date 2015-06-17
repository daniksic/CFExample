using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claudia.Site.Helpers;

namespace Claudia.Site.Areas.CMS.Controllers
{
    [Menu("CMS", 10, "Dashboard", null, null, "<span class=" + @"""glyphicon glyphicon-dashboard icon-white""" + "></span>&nbsp;Dashboard")]
    public class DashboardController : Controller
    {
        // GET: CMS/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}
