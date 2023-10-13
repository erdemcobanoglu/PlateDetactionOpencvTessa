using System;
using System.IO;
using System.Web;

namespace ImageProcessLibrary.Helper
{
    public class ProjectPathHelper
    {
        public static string GetProjectDirectory()
        {
            //if (IsWebApplication())
            //{
            //    return HttpContext.Current.Server.MapPath("~");
            //}
            //else
            //{
            //    return Directory.GetCurrentDirectory();
            //}
            return Directory.GetCurrentDirectory();
        }

        private static bool IsWebApplication()
        {
            //try
            //{
            //    return HttpContext.Current != null; 
            //}
            //catch
            //{
            //    return false;
            //}
            return false;
        }

    }
}
