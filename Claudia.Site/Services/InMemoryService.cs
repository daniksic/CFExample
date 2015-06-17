using System;
using System.Collections.Generic;
using System.Web;

namespace Claudia.Site.Services
{
    public static class InMemoryService
    {
        /// <summary>
        /// Get data from cache by string id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheId">string key</param>
        /// <param name="getItemCallback">callback function witch returns data for input</param>
        /// <param name="forceRefresh">If data exsist will be refreshed</param>
        /// <returns></returns>
        public static IEnumerable<T> Get<T>(string cacheId, Func<IEnumerable<T>> getItemCallback, bool forceRefresh = false)
        {
            IEnumerable<T> data;
            object _lock = new object();
            if (forceRefresh && HttpRuntime.Cache.Get(cacheId) != null)
            {
                lock (_lock)
                data = getItemCallback();
                HttpRuntime.Cache.Remove(cacheId);
                HttpRuntime.Cache.Insert(cacheId, data, null, DateTime.UtcNow.AddMinutes(5), TimeSpan.Zero);
                //HttpRuntime.Cache.Insert(cacheId, data, null, DateTime.UtcNow.AddSeconds(3), TimeSpan.Zero);
                return data;

            }

            lock (_lock)
            data = HttpRuntime.Cache.Get(cacheId) as IEnumerable<T>;
            if (data == null)
            {
                data = getItemCallback();
                //HttpContext.Current.Cache.Insert(cacheID, item,null, DateTime.UtcNow.AddMinutes(30),TimeSpan.Zero);
                HttpRuntime.Cache.Insert(cacheId, data, null, DateTime.UtcNow.AddMinutes(5), TimeSpan.Zero);
                //HttpRuntime.Cache.Insert(cacheId, data, null, DateTime.UtcNow.AddSeconds(3), TimeSpan.Zero);
            }
            return data;
        }

        public static void Remove(string cacheId)
        {
            HttpRuntime.Cache.Remove(cacheId);
        }

        public static void Set<T>(string cacheId, IEnumerable<T> data)
        {
            HttpRuntime.Cache.Insert(cacheId, data, null, DateTime.UtcNow.AddMinutes(5), TimeSpan.Zero);
            //HttpRuntime.Cache.Insert(cacheId, data, null, DateTime.UtcNow.AddSeconds(3), TimeSpan.Zero);
        }
    }
}
