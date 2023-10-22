using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessLibrary.Helper.CacheHelper
{
    // Implement a basic in-memory cache
    internal class InMemoryCache : ICache
    {
        private Dictionary<string, (object, DateTime)> cache = new Dictionary<string, (object, DateTime)>();

        public void Add(string key, object value, TimeSpan expiration)
        {
            DateTime expirationTime = DateTime.Now.Add(expiration);
            cache[key] = (value, expirationTime);
        }

        public object Get(string key)
        {
            if (cache.TryGetValue(key, out var cacheEntry) && cacheEntry.Item2 > DateTime.Now)
            {
                return cacheEntry.Item1;
            }
            return null;
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}
