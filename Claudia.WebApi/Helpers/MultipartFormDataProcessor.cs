using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Claudia.Domain.Configuration;
using Claudia.Repository;
using Claudia.WebApi.DTO;
using Newtonsoft.Json;
using MultipartFormDataStreamProvider = Claudia.WebApi.Providers.MultipartFormDataStreamProvider;
using Claudia.Domain.Models.v1;

namespace Claudia.WebApi.Helpers
{
    public class MultipartFormDataProcessor
    {
        public MultipartFormDataProcessor(MultipartFormDataStreamProvider data)
        {
            
                try
                {
                    var model = data.FormData.GetValues("fileAttr0").First();
                    var file = data.FileData.First();

                    ImageRequestDto = JsonConvert.DeserializeObject<ImageRequestDto>(model);

                    ImageRequestDto.FileData = file;

                }
                catch (Exception msg)
                {
                    throw new HttpResponseException(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.UnsupportedMediaType,
                        ReasonPhrase = msg.Message
                    });
                }
        }

        public ImageRequestDto ImageRequestDto { get; set; }

        /// <summary>
        /// One stop shop: Save local picture and to Db
        /// </summary>
        public void ProcessData()
        {
            var locFilNm = this.ImageRequestDto.FileData.Headers.ContentDisposition.FileName;
            var category = this.ImageRequestDto.ImageCategory;
            if (CheckIfAllreadyExists(locFilNm, category))
            {
                throw new Exception(string.Format("Picture with name '{0}' already exists!!!", locFilNm));
            }

            ProcessImage();
            //SaveToDb();
        }

        private void ProcessImage()
        {
            ImageEngine.ImageEngine.ProcessImage(this.ImageRequestDto);
        }

        [Obsolete("saving is done from breeze controler", true)]
        private void SaveToDb()
        {
            //using (var u = new UnitOfWork())
            //{
            //    var data = new Link();
            //    data.Title = this.ImageRequestDto.Title;
            //    data.Description = this.ImageRequestDto.Description;
            //    data.ServerFileName = this.ImageRequestDto.FileData.LocalFileName;
            //    data.CategoryId = 1;
            //    data.ClientLocalFileName = this.ImageRequestDto.FileData.Headers.ContentDisposition.FileName;

            //    u.Link.Add(data);
            //    u.SaveChanges();
            //}
        }

        /// <summary>
        /// Checks in Db if client local filename already exists
        /// </summary>
        /// <param name="localFileName">Client local filename /w file extension</param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private bool CheckIfAllreadyExists(string localFileName, string category)
        {
            using (var u = new UnitOfWork())
            {
                var firstOrDefault = u.Category.Find(c => c.EntityName == category).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var categoryId = firstOrDefault.Id;

                    return u.Link.Find(f => f.ClientLocalFileName == localFileName && f.IsDeleted == false && f.CategoryId == categoryId).Any();
                }

                return true;
            }
        }
    }
}