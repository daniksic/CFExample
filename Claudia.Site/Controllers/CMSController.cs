using System.Security.Cryptography;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claudia.Site.Helpers;
using WebGrease.Css.Extensions;

namespace Claudia.Site.Controllers
{
    //obsolite use area/cms
    //public class CMSController : Controller
    //{
    //    //
    //    // GET: /CMS/
    //    [HttpGet]
    //    public ActionResult Index()
    //    {
    //        //return View("ImageCrop");
    //        return View("Dashboard");
    //    }

    //    [HttpGet]
    //    public ActionResult List(string entity, bool showdeleted = false)
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var category = uow.Category.Find(y => y.EntityName == entity).FirstOrDefault();
    //            ViewBag.Title = (category !=null) ? category.Title : "gallery";
    //            ViewBag.Entity = entity;

    //            switch (entity)
    //            {
    //                case "recipe":
    //                    var rec = uow.Recipe.GetAll();
    //                    return View("_Recipe", rec);
    //                //case "gallery":
    //                //    return View("_Gallery");
    //                case "askclaudia":
    //                    return View("_AskClaudiaEdit");
    //                case "youtubefp":
    //                    return View("_YouTubeEdit");
    //                default:
    //                    var list = uow.Link.Find(g => g.ObjectCategory.EntityName == entity && g.IsDeleted == showdeleted).ToList();
    //                    return View("List", list);
    //            }
    //        }
    //    }

    //    [HttpGet]
    //    public ActionResult Create(string entity)
    //    {
    //        ViewBag.Entity = entity;
    //        ViewBag.Title = "Insert new";

    //        using (var uow = new UnitOfWork())
    //        {
    //            var category = uow.Category.Find(y => y.EntityName == entity).FirstOrDefault();
    //            if (category == null)
    //                throw new NotSupportedException(string.Format("{0} category is not supported!", entity));

    //            switch (entity)
    //            {
    //                case "recipe":
    //                    var recipe = new Recipe();
    //                    recipe.CategoryId = category.Id;
    //                    return View("_RecipeEdit", recipe);
    //                //case "gallery":
    //                //    return View("_Gallery");
    //                case "askclaudia":
    //                    return View("_AskClaudiaEdit");
    //                case "youtubefp":
    //                    return View("_YouTubeEdit");
    //                default:
    //                    var model = new Link();
    //                    model.CategoryId = category.Id;
    //                    return View("_LinkEdit", model);
    //            }
    //        }
    //    }
    //    [HttpPost]
    //    public ActionResult Create([ModelBinder(typeof(ModelResolver))] IEntity newLink)
    //    {
    //        try
    //        {
    //            // model binding works, to do other logic
    //            var d = newLink;

    //            if (ModelState.IsValid)
    //            {
    //                string entity;
    //                using (var uow = new UnitOfWork())
    //                {
    //                    //uow.Link.Add(newLink);
    //                    //uow.SaveChanges();

    //                    //entity = uow.Category.Get(newLink.CategoryId).EntityName;
    //                }
                    
    //                //return RedirectToAction("List", new {entity= entity ?? "all"});
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError(ex.Source, ex.Message);
    //        }
    //        return View("_LinkEdit",newLink);
    //    }

    //    //[HttpPost]
    //    //public ActionResult Create(Recipe newRecipe)
    //    //{
    //    //    try
    //    //    {
    //    //        if (ModelState.IsValid)
    //    //        {
    //    //            using (var uow = new UnitOfWork())
    //    //            {
    //    //                uow.Recipe.Add(newRecipe);
    //    //                uow.SaveChanges();
    //    //            }

    //    //            return RedirectToAction("List?entity=recipe");
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        ModelState.AddModelError(ex.Source, ex.Message);
    //    //    }
    //    //    return View(newRecipe);
    //    //}

    //    [HttpGet]
    //    public ActionResult Edit(int id)
    //    {
    //        Link link;
    //        using (var uow = new UnitOfWork())
    //        {
    //            link = uow.Link.Get(id);

    //            //not needed, left for demo
    //            //var categories = uow.ApplicationContext.Categories.Where(c => c.IsDeleted == false).ToList();
    //            //ViewBag.Categories = new SelectList(categories, "Id", "Title", link.CategoryId);
    //        }

    //        return View("Edit", link);
    //    }
    //    [HttpPost]
    //    public ActionResult Edit(Link newLink)
    //    {
    //        try
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                using (var uow = new UnitOfWork())
    //                {
    //                    //uow.Link.Attach(newLink);
    //                    //uow.SaveChanges();
    //                }

    //                return RedirectToAction("Index");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError(ex.Source, ex.Message);
    //        }

    //        return View(newLink);

    //    }

    //}
}
