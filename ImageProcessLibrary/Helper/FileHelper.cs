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

        /// <summary>
        /// string directory
        /// string fileName
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FindFileInProjectDirectory(string directory, string fileName)
        {
            string[] files = Directory.GetFiles(directory, fileName, SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                // Return the first matching file path
                return files[0];
            }

            return null; // File not found
        }

        /// <summary>
        /// string directory
        /// string fileName
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FindFileSubFolderInProjectDirectory(string directory, string fileName)
        {
            string[] files = Directory.GetFiles(directory, fileName, SearchOption.TopDirectoryOnly);

            if (files.Length > 0)
            {
                return files[0]; // File found in the current directory
            }

            string[] subdirectories = Directory.GetDirectories(directory);

            foreach (string subdirectory in subdirectories)
            {
                string filePath = FindFileInProjectDirectory(subdirectory, fileName);
                if (filePath != null)
                {
                    return filePath; // File found in a subdirectory
                }
            }

            return null; // File not found in this directory or its subdirectories
        }

    }
}
