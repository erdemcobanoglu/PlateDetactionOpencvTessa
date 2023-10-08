using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Tesseract;

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
             
        }
          
         
    }


}



 