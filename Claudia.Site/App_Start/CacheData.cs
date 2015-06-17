using System.Diagnostics;
using Claudia.Repository;
using Claudia.Site.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claudia.Site.Services;

namespace Claudia.Site
{
    public class CacheData
    {
        public static void LoadIntoCache()
        {
            using (var uow = new UnitOfWork())
            {
                var categoryes = uow.ApplicationContext.Categories.ToList();
                var carousels = uow.ApplicationContext.Links.Where(c => c.IsDeleted == false && c.CategoryId == 6).OrderByDescending(c => c.DateTimeStamp).ToList();
                var youtube = uow.ApplicationContext.Links.Where(y => y.IsDeleted == false && y.CategoryId == 2).OrderByDescending(y => y.DateTimeStamp).Take(1).ToList();

                if(categoryes.Count > 0) InMemoryService.Set("category", categoryes);
                if(carousels.Count > 0) InMemoryService.Set("carousels", carousels);
                if(youtube.Count > 0) InMemoryService.Set("youtube", youtube);
            }
        }
    }
}