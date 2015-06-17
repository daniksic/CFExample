using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Claudia.Domain.Models.v1;
using Claudia.Site.Services;
using PagedList;
using Claudia.Domain.Models;
using Claudia.Repository.Context;
using Claudia.Site.Helpers;
using Claudia.Repository;
using Claudia.Site.Models;

namespace Claudia.Site.Controllers
{
    [Menu("CMS", 100, "Homepage @@ CF.com", null, null, "<span class=" + @"""glyphicon glyphicon-home icon-white""" + "></span>&nbsp;Homepage @@ CF.com", "Default")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
#if (true)
            QuickData.Refresh();
#endif
            var cp = QuickData.GetCarouselPictureLinks();
            if (cp.Any())
                ViewBag.carouselModel = cp;

           
            ViewBag.Widgets = QuickData.GetWidgets();
            ViewBag.Announcement = QuickData.GetAnnouncement();

            return View();
        }

        public ActionResult About()
        {
            var cp = QuickData.GetCarouselPictureLinks();
            if (cp.Any())
                ViewBag.carouselModel = cp;

            ViewBag.Announcement = QuickData.GetAnnouncement();

            return View();
        }

        public ActionResult RecipeCategoryList()
        {
            //var cp = QuickData.GetCarouselPictureLinks();
            //if (cp.Any())
            //    ViewBag.carouselModel = cp;
            ViewBag.Announcement = QuickData.GetAnnouncement();
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            var cp = QuickData.GetCarouselPictureLinks();
            if (cp.Any())
                ViewBag.carouselModel = cp;
            ViewBag.Announcement = QuickData.GetAnnouncement();

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string subject, string message)
        {
            // tekstovi unutar funkcije
            string bodyText = "Dear Claudia, you have new message from user named '" + name + "'. His email address is '" + email + "' and the message is:\n\n " + message + "";

            using (var smtp = new Mailer("mail.camellia.arvixe.com", 25))
            {
                smtp.SmtpClient.Credentials = new NetworkCredential("fans@claudiafloraunce.com", "claudiaWeb");

                smtp.MailMessage.From = new MailAddress(email, "Contact form - Claudia Website");

                smtp.MailMessage.To.Add(new MailAddress("fans@claudiafloraunce.com"));

                smtp.MailMessage.Subject = "Message from Claudia website " + subject;

                smtp.MailMessage.Body = bodyText;

                smtp.SendMail();
            }

            return RedirectToAction("Contact");
        }

        public ActionResult SendMail(string email)
        {
            // tekstovi unutar funkcije
            string bodyText = "Dear Claudia, you have new message from your website. The person with email '" + email + "' is interested in advertising. Contact him/her on email provided and try to arrange some business.";

            using (var smtp = new Mailer("mail.camellia.arvixe.com", 25))
            {
                smtp.SmtpClient.Credentials = new NetworkCredential("fans@claudiafloraunce.com", "claudiaWeb");

                smtp.MailMessage.From = new MailAddress(email, "Advertise - Claudia Website");

                smtp.MailMessage.To.Add(new MailAddress("fans@claudiafloraunce.com"));

                smtp.MailMessage.Subject = "Advertise message";

                smtp.MailMessage.Body = bodyText;

                smtp.SendMail();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Gallery(int? page)
        {

            //var comments = db.Comments.ToList();
            //var pageNumber = page ?? 1;

            //var onePageOfComments = comments.ToPagedList(pageNumber, 20);

            //ViewBag.OnePageOfComments = onePageOfComments;

            //return View(db.Comments.ToList());
            ViewBag.Announcement = QuickData.GetAnnouncement();

            return View();
        }

        public ActionResult Recipes()
        {
            ViewBag.Announcement = QuickData.GetAnnouncement();
            return View();
        }

        public ActionResult AskClaudia()
        {
            ViewBag.Announcement = QuickData.GetAnnouncement();
            return View();
        }

        public ActionResult VideoBlog()
        {
            ViewBag.Announcement = QuickData.GetAnnouncement();
            return View();
        }

        //public ActionResult SaveComment(string name, string email, string comment)
        //{
        //    // TODO: (DN) automatizirati proces spremanja preko Repository-a
        //    try
        //    {
        //        if (ModelState.IsValid)// there is no model here!!!
        //        {
        //            Comment cmtDb = new Comment();
        //            User userName = db.Users.FirstOrDefault(u => u.UserName == name);

        //            cmtDb.CategoryId = 5;
        //            cmtDb.DateTimeStamp = DateTime.Today.ToUniversalTime();
        //            cmtDb.IsDeleted = false;
        //            cmtDb.ObjectId = 1;
        //            cmtDb.Text = comment;
        //            cmtDb.UserId = userName.Id;

        //            db.Comments.Add(cmtDb);
        //            db.SaveChanges();

        //            return RedirectToAction("Gallery");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new SystemException("There was an error with saving the comment into the database. Please try again. Message: ", ex);
        //    }

        //    return RedirectToAction("Gallery");
        //}

        #region helpers

        #endregion
    }
}