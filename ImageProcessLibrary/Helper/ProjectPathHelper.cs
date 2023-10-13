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
        
        public static string GetParentDirectory(string currentDirectory)
        { 
            string parentDirectory = Path.GetDirectoryName(currentDirectory);

            if (parentDirectory != null)
            {
                return parentDirectory;
            }
            else
            {
                throw new InvalidOperationException("Cannot access parent directory. It doesn't exist.");
            }
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
