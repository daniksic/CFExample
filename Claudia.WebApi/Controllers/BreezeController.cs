using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.WebApi2;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;
using Claudia.Repository;
using Newtonsoft.Json.Linq;

namespace Claudia.WebApi.Controllers
{
    [BreezeController]
    public class BreezeController : ApiController
    {
        private UnitOfWork _data = new UnitOfWork();

        [HttpGet]
        public string Metadata()
        {
            //using (var _data = new UnitOfWork()) 
            //{
            return _data.BreezeRepository.Metadata();
            //}
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            //using (var _data = new UnitOfWork()) 
            //{
            return _data.BreezeRepository.SaveChanges(saveBundle);
            //}
        }

        [HttpGet]
        public IQueryable<Link> Links()
        {
            //using (var _data = new UnitOfWork()) // ERROR for using after successfuly get Metadata, exception that repository already disposed
            //{
            return _data.BreezeRepository.Links;
            //}
        }

        [HttpGet]
        public IQueryable<Recipe> Recipes()
        {
            //using (var _data = new UnitOfWork()) // ERROR for using after successfuly get Metadata, exception that repository already disposed
            //{
            return _data.BreezeRepository.Recipes;
            //}
        }


        [HttpGet]
        public IQueryable<Comment> Comments()
        {
            //using (var _data = new UnitOfWork())
            //{
            return _data.BreezeRepository.Comments;
            //}
        }
    }
}
