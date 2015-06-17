using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using Claudia.Repository.Context;
using Claudia.Site.Areas.CMS.Models;
using Claudia.Site.Helpers;
using Claudia.Site.Models;
using PagedList;
using WebGrease.Css.Extensions;

namespace Claudia.Site.Areas.CMS.Controllers
{
    [Menu("CMS", 30, "Recipes", null, null, "<span class=" + @"""glyphicon glyphicon-list-alt icon-white""" + "></span>&nbsp;Recipes")]
    public class RecipeController : Controller
    {
        private static readonly ObjectCategory ENTITYCATEGORY = QuickData.GetEntitesCategories().FirstOrDefault(c => c.EntityName == "recipe");
        private readonly IUnitOfWork _uow;
        private readonly int PAGESIZE = 2; //todo move to config

        public RecipeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CMS/Recipe
        public ActionResult Index(int page = 1)
        {
            IQueryable<LinkRecipeViewModel> data = null;
            try
            {
                data =
                    (from link in
                        _uow.Link.Find(g => g.CategoryId == ENTITYCATEGORY.Id && g.IsDeleted == false)
                            .Include(x => x.LinksUrls)
                        join recipe in _uow.Recipe.Find(g => g.IsDeleted == false).Include(i => i.Category) on link.Id
                            equals recipe.LinkId
                        select new LinkRecipeViewModel
                        {
                            Link = link,
                            Recipe = recipe
                        }).OrderByDescending(x => x.Link.DateTimeStamp);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            var pagedList = data.ToPagedList(page, PAGESIZE);

            return View(pagedList);
        }

        // GET: CMS/Recipe/Create
        public ActionResult Create()
        {
            var model = new LinkRecipeViewModel();
            //model.CreatorUser.Id = "ce6d330c-0b7d-4e55-83ad-b8999bf3bdaf";//"3df70c16-d7e5-4b63-87e0-345084056ae9";
            model.Link.CategoryId = ENTITYCATEGORY.Id;

            var query = _uow.RecipeCategory.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });

            ViewBag.RecipeCategoryItems = query;

            return View(model);
        }
        
        // POST: CMS/Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LinkRecipeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _uow.Link.Add(model.Link);
                    _uow.Recipe.Add(model.Recipe);
                    _uow.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            var query = _uow.RecipeCategory.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });

            ViewBag.Items = query;
            return View(model);
        }

        // GET: CMS/Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            var model = (from link in
                _uow.Link.Find(g => g.Id == id && g.IsDeleted == false)
                    .Include(x => x.LinksUrls)
                join recipe in _uow.Recipe.Find(g => g.IsDeleted == false).Include(i => i.Category) on link.Id
                    equals recipe.LinkId
                select new LinkRecipeViewModel
                {
                    Link = link,
                    Recipe = recipe
                }).FirstOrDefault();

            return View(model);
        }

        // POST: CMS/Recipe/Edit/5
        [HttpPost]
        public ActionResult Edit(LinkRecipeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _uow.Link.Attach(model.Link);
                    _uow.Recipe.Attach(model.Recipe);
                    _uow.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View(model);
        }

        // GET: CMS/Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _uow.Recipe.Get(id);

            return View(model);
        }

        // POST: CMS/Recipe/Delete/5
        [HttpPost]
        public ActionResult Delete(Recipe model)
        {
            try
            {
                _uow.Recipe.Remove(model);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "There was and error while deleting the recipe. Please try again");
            }
            return View(model);
        }
    }
}
