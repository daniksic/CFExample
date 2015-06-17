using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claudia.Site.Helpers;

namespace Claudia.Site.Areas.CMS.Controllers
{
    [Menu("CMS", 50, "YouTubeVideos", null, null, "<span class=" + @"""glyphicon glyphicon-list-alt icon-white""" + "></span>&nbsp;Videos")]
    public class YTVideoController : Controller
    {
        // GET: CMS/YTVideo
        public ActionResult Index()
        {
            return View();
        }

        // GET: CMS/YTVideo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CMS/YTVideo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CMS/YTVideo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CMS/YTVideo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CMS/YTVideo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CMS/YTVideo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CMS/YTVideo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
