using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using plateRecognize.Helper;
using System;
using System.Drawing;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace plateRecognize
{
    public static class TessaV2Extension
    {
        public static void StartProcess(this string imagePath, string tessDataPath, string imageFolder, string saveFolder)
        {
            try
            {
                using (var image = CvInvoke.Imread(tessDataPath + imageFolder))
                {
                    if (image.IsEmpty)
                    {
                        Console.WriteLine("Error: Unable to load the image.");
                        return;
                    }

                    var grayImage = new Mat();
                    CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

                    var edges = new Mat();
                    CvInvoke.Canny(grayImage, edges, 100, 200);

                    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                    CvInvoke.FindContours(edges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                      
                    PerformOcrSimpleProcessHelper(tessDataPath, imagePath);
                    PerformOcrComplexProcessHelper(tessDataPath, imagePath, image, contours, saveFolder); 
                }
            }
            catch (Exception ex)
            {
                Console.Write("test");
            }
        }

        public static string PerformOcrSimpleProcessHelper(string tessDataPath, string imagePath)
        {
            try
            {
                using (var tesseractEngine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    tesseractEngine.SetVariable("TESSDATA_PREFIX", tessDataPath);

                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = tesseractEngine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }

        public static string PerformOcrComplexProcessHelper(string tessDataPath, string imagePath, Mat image, VectorOfVectorOfPoint contours, string saveFolder)
        {
            try
            {
                string roiImagePath = tessDataPath + saveFolder;

                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    engine.DefaultPageSegMode = PageSegMode.SingleBlock;

                    foreach (var contour in contours.ToArrayOfArray())
                    {
                        var roi = new Mat(image, CvInvoke.BoundingRectangle(contour)); 
                        CvInvoke.Imwrite(roiImagePath, roi);

                        using (var imgToRecognize = Pix.LoadFromFile(roiImagePath))
                        {
                            using (var page = engine.Process(imgToRecognize))
                            {
                                string recognizedText = page.GetText();
                                if (recognizedText.Trim().Length > 5)
                                {
                                    //Console.WriteLine("License Plate: " + recognizedText.Trim());
                                    Console.WriteLine(LicensePlateValidator.ValidateIrishLicensePlate(recognizedText.Trim()));
                                }
                               
                                
                            }
                        }
                    }
                }

                CvInvoke.Imshow("License Plate Detection", image);
                CvInvoke.WaitKey(0); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return string.Empty;
        }

    }

}
