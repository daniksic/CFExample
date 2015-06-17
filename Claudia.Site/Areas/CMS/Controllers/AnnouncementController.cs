using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using Claudia.Site.Areas.CMS.Models;
using Claudia.Site.Helpers;
using PagedList;

namespace Claudia.Site.Areas.CMS.Controllers
{
    [Menu("CMS", 40, "Announcements", null, null, "<span class=" + @"""glyphicon glyphicon-list-alt icon-white""" + "></span>&nbsp;Announcements")]

    public class AnnouncementController : Controller
    {
        private static readonly ObjectCategory ENTITYCATEGORY = QuickData.GetEntitesCategories().FirstOrDefault(c => c.EntityName == "announcement");
        private readonly IUnitOfWork _uow;
        private readonly int PAGESIZE = 2; //todo move to config

        public AnnouncementController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CMS/Announcement
        public ActionResult Index(int page = 1)
        {
            var data = _uow.Link.Find(g => g.CategoryId == ENTITYCATEGORY.Id).OrderByDescending(o => o.DateTimeStamp)
                .Select(ann => new LinkViewModel()
                {
                    Id = ann.Id,
                    Description = ann.Description,
                    Title = ann.Title
                });

            return View(data.ToPagedList(page, PAGESIZE));
        }

        // GET: CMS/Announcement/Create
        public ActionResult Create()
        {
            return View(new Link());
        }

        // POST: CMS/Announcement/Create
        [HttpPost]
        public ActionResult Create(Link model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _uow.Link.Add(model);
                    _uow.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "There was an error in the model. Please review all the fields and try again.");
            }

            return View(model);
        }

        // GET: CMS/Announcement/Edit/5
        public ActionResult Edit(int id)
        {
            var query = _uow.Link.Get(id);

            return View(query);
        }

        // POST: CMS/Announcement/Edit/5
        [HttpPost]
        public ActionResult Edit(Link model)
        {
            try
            {
                _uow.Link.Attach(model);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "There was an error while saving the data to database. Please review all the fields and try again.");
            }
            return View(model);
        }

        // GET: CMS/Announcement/Delete/5
        public ActionResult Delete(int id)
        {
            var query = _uow.Link.Get(id);

            return View(query);
        }

        // POST: CMS/Announcement/Delete/5
        [HttpPost]
        public ActionResult Delete(Link model)
        {
            try
            {
                _uow.Link.Remove(model);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "There was an error while deleting the announcement. Please try again or report to your administrator.");
            }
            return View();
        }
    }
}
