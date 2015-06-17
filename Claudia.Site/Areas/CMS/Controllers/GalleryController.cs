using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using Claudia.Site.Areas.CMS.Models;
using Claudia.Site.Helpers;
using Claudia.Site.Interfaces;
using Claudia.Site.Models;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace Claudia.Site.Areas.CMS.Controllers
{
    [Menu("CMS", 20, "Gallery", null, null, "<span class=" + @"""glyphicon glyphicon-picture icon-white""" + "></span>&nbsp;Gallery")]
    public class GalleryController : Controller
    {
        private static readonly ObjectCategory ENTITYCATEGORY = QuickData.GetEntitesCategories().FirstOrDefault(c => c.EntityName == "gallery");
        private readonly IUnitOfWork _uow;
        private readonly int PAGESIZE = 2; //todo move to config

        public GalleryController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        // GET: CMS/Gallery
        public ActionResult Index(int page = 1)
        {
            var data =
                _uow.Link.Find(g => g.CategoryId == ENTITYCATEGORY.Id && g.IsDeleted == false)
                    .Include(k => k.LinksUrls)
                    .OrderByDescending(y => y.DateTimeStamp)
                    .Select(img => new LinkViewModel()
            {
                Id = img.Id,
                Description = img.Description,
                Title = img.Title,
                Urls = img.LinksUrls
            });

            var pagedList = data.ToPagedList(page, PAGESIZE);

            return View(pagedList);
        }

        // GET: CMS/Gallery/Create
        public ActionResult Create()
        {
            var model = new LinkViewModel { CategoryName = ENTITYCATEGORY.EntityName, CategoryId = ENTITYCATEGORY.Id };
            return View(model);
        }

        // POST: CMS/Gallery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkIfExist = _uow.Link.Find(m => m.CategoryId == model.CategoryId && m.Title == model.Title).Any();
                if (checkIfExist)
                {
                    ModelState.AddModelError("Title", new DuplicateNameException());
                    return View(model);
                }

                var mappedModel = Mapper.Map<Link>(model);

                _uow.Link.Add(mappedModel);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: CMS/Gallery/Edit/5
        public ActionResult Edit(int id)
        {
            var data =
                _uow.Link.Find(g => g.Id == id).Include(k => k.LinksUrls).FirstOrDefault();

            var lvm = Mapper.Map<LinkViewModel>(data);

            return View(lvm);
        }

        // POST: CMS/Gallery/Edit/5
        [HttpPost]
        public ActionResult Edit(LinkViewModel data)
        {
            if (!ModelState.IsValid) return View(data);

            var linkFromDb = _uow.Link.Get(data.Id);

            Mapper.Map(data, linkFromDb);

            _uow.Link.Attach(linkFromDb);
            _uow.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CMS/Gallery/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _uow.Link.Get(id);
            var clink = Mapper.Map<LinkViewModel>(data);
            return View(clink);
        }

        // POST: CMS/Gallery/Delete/5
        [HttpPost]
        public ActionResult Delete(LinkViewModel data)
        {
            var link = _uow.Link.Get(data.Id);
            _uow.Link.Remove(link);

            var urls = _uow.LinkUrls.Find(u => u.LinkId == data.Id).ToList();
            urls.ForEach(item => _uow.LinkUrls.Remove(item));

            _uow.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
