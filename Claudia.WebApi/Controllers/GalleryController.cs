using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using Claudia.Domain.Configuration;
using Claudia.WebApi.DTO;
using Claudia.WebApi.Helpers;
using ImageResizer;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using ImageResizer.Configuration;
using ImageResizer.Plugins;
using ImageResizer.Plugins.Basic;
using Newtonsoft.Json;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using System.Collections.Generic;

namespace Claudia.WebApi.Controllers
{
    [RoutePrefix("api/gallery")]
    public class GalleryController : ApiController
    {
        [HttpPost, Route("new")]
        public async Task<ImageResponseDto> SavePicture()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }


            // Create a stream provider for setting up output streams that saves the output
            // If you want full control over how the stream is saved then derive from MultipartFormDataStreamProvider
            // and override what you need.
            string root = HostingEnvironment.MapPath(AppConfig.GalleryImages.FilePathRelative);
            if (!Directory.Exists(root)) { Directory.CreateDirectory(root); }

            var streamProvider = new Claudia.WebApi.Providers.MultipartFormDataStreamProvider(root);

            // Read the MIME multipart content using the stream provider we just created.
            var bodyparts = await Request.Content.ReadAsMultipartAsync(streamProvider)
                .ContinueWith(t =>
                   {
                       if (t.IsFaulted || t.IsCanceled)
                           throw new HttpResponseException(HttpStatusCode.InternalServerError);

                       try
                       {
                           var m = new MultipartFormDataProcessor(t.Result);
                           m.ProcessData();
                       }
                       catch (Exception msg)
                       {
                           throw new HttpResponseException(new HttpResponseMessage
                           {
                               StatusCode = HttpStatusCode.InternalServerError,
                               ReasonPhrase = msg.Message
                           });
                       }
                       return t;
                   }
                );

            //TODO make it type safe
            return new ImageResponseDto()
            {
                ServerFileName = bodyparts.Result.FileData.FirstOrDefault().LocalFileName,
                ClientLocalFileName = bodyparts.Result.FileData.FirstOrDefault().Headers.ContentDisposition.FileName
            };
        }

        ////get a temp image from bytes, instead of loading from disk
        ////data:image/gif;base64,
        ////this image is a single pixel (black)
        //byte[] bytes = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");

        //Image image;
        //using (MemoryStream ms = new MemoryStream(bytes))
        //{
        //    image = Image.FromStream(ms);
        //}

        //return image;

        [HttpGet, Route("getpictures")]
        public string GetPictureLinks()
        {
            using (var data = new UnitOfWork())
            {
                var links = data.Link.GetAll().Where(l=> l.CategoryId == 1);
                var x = (from l in links
                     select new
                     {
                         image = AppConfig.GalleryImages.GetImageUrl(l.ServerFileName),
                         thumb = AppConfig.GalleryImages.GetThumbUrl(l.ServerFileName)
                     });

                return JsonConvert.SerializeObject(x.ToList());
            }
        }
    }
}
