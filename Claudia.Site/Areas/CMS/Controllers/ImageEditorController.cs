using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Claudia.Domain.Configuration;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claudia.Site.Services;
using RedirectResult = System.Web.Http.Results.RedirectResult;

namespace Claudia.Site.Areas.CMS.Controllers
{
    public class ImageEditorController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ImageEditorController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CMS/ImageEditor
        public ActionResult Create(int linkId, string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;

            return View(new Models.ImageUploadViewModel() { LinkId = linkId });
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Create(Models.ImageUploadViewModel model, string returnUrl)
        {

            if (ModelState.IsValid && returnUrl != null)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            var entityName =
                _uow.Link.Find(x => x.Id == model.LinkId)
                    .Include(i => i.ObjectCategory)
                    .Select(s => s.ObjectCategory.EntityName).SingleOrDefault();

            if (string.IsNullOrEmpty(entityName))
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            var savedFileName = await ImageCreationService.ProcessImageAsync(entityName, model.ImageUpload,
                model.GetPictureCropCords());

            var galleryLinkUrl = new LinksUrl()
                {
                    LinkId = model.LinkId,
                    ServerFileName = savedFileName,
                    ClientLocalFileName = model.ImageUpload.FileName
                };

            _uow.LinkUrls.Add(galleryLinkUrl);
            _uow.SaveChanges();


            return Redirect(returnUrl);
        }
    }
}