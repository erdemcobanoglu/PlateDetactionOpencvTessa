using Model.Enums;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessLibrary.Helper.CacheHelper
{
    public static class CacheFactory
    {
        // Factory for creating different cache implementations
        public static ICache CreateCache(CacheType cacheType)
        {
            switch (cacheType)
            {
                case CacheType.InMemory:
                    return new InMemoryCache();
                case CacheType.Redis:
                    return new RedisCache();
                // Add more cache types as needed
                default:
                    throw new ArgumentException("Invalid cache type");
            }
        }
    }
}


/*    
 *    ###  how to use  ###  
 // Use the caching factory to create a cache

        ICache cache = CacheFactory.CreateCache(CacheType.InMemory);

        // Use the cache
        cache.Add("myKey", "myValue", TimeSpan.FromMinutes(10));
        var cachedValue = cache.Get("myKey");
        Console.WriteLine("Cached Value: " + cachedValue);

        // You can switch to a different cache type like Redis by changing the parameter
        cache = CacheFactory.CreateCache(CacheType.Redis);

        // Use the Redis cache
        // ...
 
 */
