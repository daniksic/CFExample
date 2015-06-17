using System.Collections.Specialized;
using System.Linq;
using Claudia.Repository;
using Nancy;
using Linq2Rest;
using System.Collections.Generic;
using Nancy.OData;
using Claudia.Domain.Models.v1;
namespace Claudia.Web.Nancyfx.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };


            Get["/api/metadata"] = parameters =>
            {
                using (var data = new UnitOfWork())
                {
                    return data.BreezeRepository.Metadata;
                }
            };

            Get["/api/links"] = parameters =>
            {
                var data = new UnitOfWork();
                var patams = Request.Query;
                var filter = new NameValueCollection();// { { "$filter", "IsDeleted eq true" } };

                foreach (var item in patams)
                {
                    filter.Add(item, patams[item]);
                }

                var result = this.Context.ApplyODataUriFilter(data.BreezeRepository.Links);

                //var result = data.BreezeRepository.Links.Filter(filter);
                return Response.AsJson(result);
            };
        }
    }
}