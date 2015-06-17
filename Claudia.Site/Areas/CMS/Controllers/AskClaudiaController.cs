using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claudia.Domain.Models.v1;
using Claudia.Site.Helpers;
using Claudia.Repository;
using Claudia.Site.Models;
using PagedList;

namespace Claudia.Site.Areas.CMS.Controllers
{
    [Menu("CMS", 40, "Ask Claudia", null, null, "<span class=" + @"""glyphicon glyphicon-bullhorn icon-white""" + "></span>&nbsp;Ask Claudia")]
    public class AskClaudiaController : Controller
    {
        // GET: CMS/AskClaudia
        public ActionResult Index(int page = 1)
        {
            using (var x = new UnitOfWork())
            {
                var data =
                    x.AskClaudia.Find(e => e.IsDeleted == false)
                        .OrderByDescending(o => o.Id)
                        .Select(ent => new AskClaudiaViewModel()
                        {
                            Id = ent.Id,
                            DateTimeStamp =  ent.DateTimeStamp,
                            AnswerDate = ent.AnswerDate,
                            IsPrivate = ent.IsPrivate,
                            IsAnswered = ent.IsAnswered
                        });

                var pagedList = data.ToPagedList(page, 2);

                return View(pagedList);
            }
        }

        // GET: CMS/AskClaudia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CMS/AskClaudia/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AskClaudia model)
        {
            try
            {
                var db = new UnitOfWork();
                db.AskClaudia.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "There was an error while saving changed to the database. Please try again.");
            }
            return View(model);
        }
    }
}
