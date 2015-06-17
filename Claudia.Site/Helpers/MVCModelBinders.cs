using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claudia.Domain.Models.v1;

namespace Claudia.Site.Helpers
{
    public class HttpPostedFileBaseModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpPostedFileBase theFile =
                controllerContext.RequestContext.HttpContext.Request.Files[bindingContext.ModelName];
            return theFile;
        }
    }
}