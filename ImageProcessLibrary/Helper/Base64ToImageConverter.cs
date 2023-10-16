using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImageProcessLibrary.Helper
{
    public class Base64ToImageConverter
    {
        public static Image ConvertBase64ToImage(string base64String)
        {
            try
            {
                // Convert the base64 string to a byte array
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Create a MemoryStream from the byte array
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // Create an Image from the MemoryStream
                    Image image = Image.FromStream(ms);
                    return image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting base64 to image: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Save Image And return image path 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public static string SaveImageToFile(Image image, string outputPath)
        {
            Guid imageGuid = Guid.NewGuid();
            StringBuilder sbResult = new StringBuilder(string.Empty);
            StringBuilder sbImagePath = new StringBuilder(outputPath);
             
            if (image != null)
            {
                try
                {
                    // add image name 
                    //image.Save(outputPath);

                    #region altta using ge aldim 
                    //Bitmap bitmap = new Bitmap(image);
                    //bitmap.Save(outputPath); 
                    #endregion

                    using (Bitmap bitmap = new Bitmap(image))
                    {
                        bitmap.Save(sbImagePath.Append("\\" + imageGuid).ToString() + ".jpg"); 
                    }

                    sbResult.Append(imageGuid);
                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    Console.WriteLine("GDI+ Error: " + ex.Message);
                    // Handle the exception as needed
                }
                
            }
            return sbResult.ToString();
        }

        public static void SaveImageToPathBitmap(byte[] imageBytes, string outputPath)
        {
            try
            {
                File.WriteAllBytes(outputPath, imageBytes);
                Console.WriteLine("Image saved to: " + outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving image: " + ex.Message);
            }
        }
    }
}
