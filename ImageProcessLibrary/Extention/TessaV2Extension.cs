using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ImageProcessLibrary.Helper;
using plateRecognize.Helper;
using System;
using System.Drawing;
using System.Text;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace plateRecognize
{
    public static class TessaV2Extension
    {
        /// <summary>
        /// Path to the file you want to create or open
        /// </summary>
        /// <param name="filePath"></param>
        public static void CheckFileValidation()
        {
            StringBuilder sb = new StringBuilder();

            var filePath = ProjectPathHelper.GetProjectDirectory();
            var folderName = "\\ImageSaveProcess";

            sb.Append(filePath);
            sb.Append(folderName);

            
            FileHelper.CreateIfNotExists(sb.ToString());
        }

        /// <summary>
        /// Start Image Process
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="tessDataPath"></param>
        /// <param name="imageFolder"></param>
        /// <param name="saveFolder"></param>
        /// <returns></returns>
        public static string StartProcess(this string imagePath, string tessDataPath, string imageFolder, string saveFolder)
        {
            var result = string.Empty;
            CheckFileValidation();

            try
            {
                using (var image = CvInvoke.Imread(tessDataPath + imageFolder))
                {
                    if (image.IsEmpty)
                    {
                        return "Error: Unable to load the image.";
                    }

                    var grayImage = new Mat();
                    CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

                    var edges = new Mat();
                    CvInvoke.Canny(grayImage, edges, 100, 200);

                    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                    CvInvoke.FindContours(edges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                      
                    var date = PerformOcrSimpleProcessHelper(tessDataPath, imagePath);
                    var  plate = PerformOcrComplexProcessHelper(tessDataPath, imagePath, image, contours, saveFolder);

                    result = date == string.Empty ? plate : date;
                }
            }
            catch (Exception ex)
            {
               // Log  Console.Write("test");
            }
            return result;
        }

        /// <summary>
        /// PerformOcrSimpleProcessHelper
        /// </summary>
        /// <param name="tessDataPath"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static string PerformOcrSimpleProcessHelper(string tessDataPath, string imagePath)
        {
            var result = string.Empty;

            try
            {
                using (var tesseractEngine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    tesseractEngine.SetVariable("TESSDATA_PREFIX", tessDataPath);

                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = tesseractEngine.Process(img))
                        {
                            if (page.GetText().Length > 5)
                                result = page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log  Console.WriteLine(ex); 
            }
            return result;
        }

        /// <summary>
        /// If PerformOcrSimpleProcessHelper cannot find it, let it run
        /// </summary>
        /// <param name="tessDataPath"></param>
        /// <param name="imagePath"></param>
        /// <param name="image"></param>
        /// <param name="contours"></param>
        /// <param name="saveFolder"></param>
        /// <returns></returns>
        public static string PerformOcrComplexProcessHelper(string tessDataPath, string imagePath, Mat image, VectorOfVectorOfPoint contours, string saveFolder)
        {
            var _plate = string.Empty;
            try
            {
                string roiImagePath = tessDataPath + saveFolder;
                LetterCounter letterCounter = new LetterCounter();

                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    engine.DefaultPageSegMode = PageSegMode.SingleBlock;

                    foreach (var contour in contours.ToArrayOfArray())
                    {
                        var roi = new Mat(image, CvInvoke.BoundingRectangle(contour)); 
                        CvInvoke.Imwrite(roiImagePath, roi);

                        using (var imgToRecognize = Pix.LoadFromFile(roiImagePath))
                        using (var page = engine.Process(imgToRecognize))
                        {
                            string recognizedText = page.GetText().Trim();
                            if (recognizedText.Length > 5)
                            { 
                                _plate = LicensePlateValidator.ValidateIrishLicensePlate(recognizedText.Trim());

                                if (_plate != null) break; 
                            }
                        }

                    }
                }

                // dont necessary show image 
                //CvInvoke.Imshow("License Plate Detection", image);
                //CvInvoke.WaitKey(0); 
            }
            catch (Exception ex)
            {
                // Log   Console.WriteLine(ex);
            }
            return _plate;
        }

         

    }

}
