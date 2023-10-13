using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Tesseract;
using System.Drawing;

namespace plateRecognize
{
    class Program
    {
        static void Main(string[] args)
        {
            string imagePath = "C://Users//erdem//Documents//test-pic//tessa-test2.jpg";
            string tessFodler = "C://Users//erdem//source//repos//plateRecognize//plateRecognize//bin//Debug//net5.0//tessdata";
            string imageFolder = "//tet-car11111.jpg"; // "//22111111.jpg"; // calisan ornek "//2211.jpg";  //tet-car11111.jpg
            string saveFolder = "//res//" + "1.jpg";

            imagePath.StartProcess(tessFodler, imageFolder, saveFolder);


            

            #region test icin
            string base64String = "YourBase64StringHere"; // Replace with your actual base64 string
           // Image image = Base64ToImageConverter.ConvertBase64ToImage(base64String);

            //if (image != null)
            //{
            //    // Save the image to a file (optional)
            //   // Base64ToImageConverter.SaveImageToFile(image, "output.png");

            //    // You can also work with the 'image' object as needed
            //    // For example, display it in a PictureBox or process it further.
            //} 
            #endregion

        }
          
         
    }


}



 