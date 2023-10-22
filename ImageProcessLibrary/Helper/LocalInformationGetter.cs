using Model;
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

        public Localinfo ReadJsonConfig(string jsonFileName)
        {
            var filePath = FileHelper.FindFileSubFolderInProjectDirectory(ProjectPathHelper.GetParentDirectory(ProjectPathHelper.GetProjectDirectory()), jsonFileName); 

            return JsonSerializer.Deserialize<Localinfo>(File.ReadAllText(filePath));
        }
    }
}
