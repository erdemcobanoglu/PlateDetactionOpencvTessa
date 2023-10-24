using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessLibrary.Helper.CacheHelper
{
    // Implement a Redis cache
    public class RedisCache : ICache
    {
        // Implement the Redis caching logic here
        public void Add(string key, object value, TimeSpan expiration)
        {
            // Add implementation for Redis caching
        }

        public object Get(string key)
        {
            // Add implementation for Redis caching
            return null;
        }

        public void Remove(string key)
        {
            // Add implementation for Redis caching
        }
    }
}
