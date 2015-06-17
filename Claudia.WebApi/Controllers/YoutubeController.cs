using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;

namespace Claudia.WebApi.Controllers
{
    [RoutePrefix("api/youtube")]
    public class YoutubeController : ApiController
    {
        //[Route("list"), HttpGet]
        //public IEnumerable<Link> GetYouTubeList()
        //{
        //    return new List<Link>
        //    {
        //        //new Link {Url = "http://www.youtube.com/embed/WaNYnBkEhkk", Title = "Plus size Model Claudia Floraunce at LS Fashion..."},
        //        new Link {Url = "https://i.ytimg.com/vi/WaNYnBkEhkk/hqdefault.jpg", Title = "only piture from yubito"},
        //        new Link {Url = "http://img.youtube.com/vi/WaNYnBkEhkk/0.jpg", Title = "only piture from yubito"}
        //    };
        //}

        //[Route("add/{list}"), HttpPost]
        //public HttpResponseMessage AddYouTubes(IEnumerable<Link> list)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            //todo: save to db

        //            return Request.CreateResponse(HttpStatusCode.OK, list.ToString());
        //        }
        //        catch (Exception e)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
        //        }
                
        //    }

        //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
        //}

    }
}
