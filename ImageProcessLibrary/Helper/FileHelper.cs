using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageProcessLibrary.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// If the relevant file does not exist, create it
        /// </summary>
        /// <param name="Path"></param>
        public static void CreateIfNotExists(string Path)
        {
            if (!Directory.Exists(Path))
            {
                // If the folder doesn't exist, create it
                Directory.CreateDirectory(Path);
            }
            else
            {
                Console.WriteLine("File already exists.");
            }

            #region MyRegion
            //if (!File.Exists(Path))
            //{
            //    // If the file doesn't exist, create it
            //    using (File.Create(Path))
            //    {
            //        Console.WriteLine("File created successfully.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("File already exists.");
            //} 
            #endregion

        }
         
    }
}
