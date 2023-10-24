using ImageProcessLibrary.Helper.CacheHelper;
using Model;
using Model.Enums;
using Model.Interfaces;
using System;
using System.Configuration;
using System.IO;
using System.Text.Json;

namespace ImageProcessLibrary.Helper
{
    public class LocalInformationGetter
    {
        // ToDo Add Cache 
        static Localinfo ReadAppConfig()
        {
            //string setting1 = ConfigurationManager.AppSettings["Setting1"];
            //string setting2 = ConfigurationManager.AppSettings["Setting2"];

            //return new Model.Localinfo { Setting1 = setting1, Setting2 = setting2 };
            return new Model.Localinfo { };
        }

        private Localinfo ReadJsonConfig(string jsonFileName)
        {  
            var filePath = FileHelper.FindFileSubFolderInProjectDirectory(ProjectPathHelper.GetParentDirectory(ProjectPathHelper.GetProjectDirectory()), jsonFileName);

            return JsonSerializer.Deserialize<Localinfo>(File.ReadAllText(filePath)); 
        }

        /// <summary>
        /// fileName cache key
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="localinfo"></param>
        /// <returns></returns>
        public Localinfo CheckAndGetMemoryData(string cacheKey)
        {
            // Use the caching factory to create a cache
            ICache cache = CacheFactory.CreateCache(CacheType.InMemory);

            // Try to get the value from the cache
            var cachedValue = cache.Get(cacheKey);

            if (cachedValue == null)
            {
                // get data 
                var localinfo = ReadJsonConfig(cacheKey);

                // Value is not in the cache, so add it 
                cache.Add(cacheKey, localinfo, TimeSpan.FromMinutes(60));

                // Now, cachedValue will hold the newly added value
                cachedValue = localinfo; 
            }

            // Use cachedValue or return it as needed
            return cachedValue as Localinfo;
        }
    }
}
