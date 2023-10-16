using System;
using System.Drawing;
using System.IO;

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

        public static void SaveImageToFile(Image image, string outputPath)
        {
            if (image != null)
            {
                try
                {

                    //image.Save(outputPath);
                    Bitmap bitmap = new Bitmap(image);
                    bitmap.Save(outputPath);

                    Console.WriteLine("Image saved to: " + outputPath);
                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    Console.WriteLine("GDI+ Error: " + ex.Message);
                    // Handle the exception as needed
                }
            }
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
