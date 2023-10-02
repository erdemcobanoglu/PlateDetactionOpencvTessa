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
            tessaV2();
        }

        static void videoRecordTest()
        {
            // Specify the path to the image file
            string imagePath = "C://Users//erdem//Documents//test-pic"; // Replace with the actual file path

            // Load an image from the specified file
            using (var image = CvInvoke.Imread(imagePath))
            {
                // Check if the image is valid
                if (image.IsEmpty)
                {
                    Console.WriteLine("Error: Unable to load the image.");
                    return;
                }

                // Perform license plate detection (you'll need a trained model for this)
                // This example only shows basic edge detection
                var grayImage = new Mat();
                CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

                var edges = new Mat();
                CvInvoke.Canny(grayImage, edges, 100, 200);

                // Find contours in the edges image
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(edges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                // Draw the detected contours on the original image
                CvInvoke.DrawContours(image, contours, -1, new MCvScalar(0, 0, 255), 2);

                // Display the result
                CvInvoke.Imshow("License Plate Detection", image);

                // Wait for a key press to close the window
                CvInvoke.WaitKey(0);
            }
        }

        static void videoTestGeneric()
        {
            // Load an image from a file or capture it from a camera
            using (var capture = new VideoCapture(0))
            using (var image = new Mat())
        {
            while (true)
            {
                // Capture a frame
                capture.Read(image);

                // Check if the image is valid
                if (image.IsEmpty)
                {
                    Console.WriteLine("Error: Unable to capture image.");
                    break;
                }

                // Perform license plate detection (you'll need a trained model for this)
                // This example only shows basic edge detection
                var grayImage = new Mat();
                CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

                var edges = new Mat();
                CvInvoke.Canny(grayImage, edges, 100, 200);

                // Find contours in the edges image
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(edges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                // Draw the detected contours on the original image
                CvInvoke.DrawContours(image, contours, -1, new MCvScalar(0, 0, 255), 2);

                // Display the result
                CvInvoke.Imshow("License Plate Detection", image);

                // Break the loop when a key is pressed (e.g., press 'q' to quit)
                if (CvInvoke.WaitKey(1) == 'q')
                    break;
            }
        }
        }

        static void tessaTest()
        {
            // Specify the path to the image file
            string imagePath = "C://Users//erdem//Documents//test-pic"; // Replace with the actual file path

            // Load an image from the specified file
            using (var image = CvInvoke.Imread(imagePath))
            {
                // Check if the image is valid
                if (image.IsEmpty)
                {
                    Console.WriteLine("Error: Unable to load the image.");
                    return;
                }

                // Perform license plate detection (you'll need a trained model for this)
                // This example only shows basic edge detection
                var grayImage = new Mat();
                CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

                var edges = new Mat();
                CvInvoke.Canny(grayImage, edges, 100, 200);

                // Find contours in the edges image
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(edges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                // Loop through the detected contours
                foreach (var contour in contours.ToArrayOfArray())
                {
                    // You may need more advanced filtering here to identify the license plate region

                    // Create a region of interest (ROI) around the contour
                    var roi = new Mat(image, CvInvoke.BoundingRectangle(contour));

                    // Perform OCR on the ROI using Tesseract
                    using (var engine = new TesseractEngine(@"path_to_tessdata_folder", "eng", EngineMode.Default))
                    {
                        engine.DefaultPageSegMode = PageSegMode.SingleBlock;
                        //using (var page = engine.Process(roi))
                        //{
                        //    // Get the recognized text
                        //    string recognizedText = page.GetText();

                        //    // Print the recognized text on the screen
                        //    Console.WriteLine("License Plate: " + recognizedText.Trim());
                        //}
                    }
                }

                // Display the result image with contours (optional)
                CvInvoke.Imshow("License Plate Detection", image);

                // Wait for a key press to close the window
                CvInvoke.WaitKey(0);
            }
        }

        static void tessaV2()
        {
            #region 1
            //// Specify the path to the image file
            //string imagePath = "path_to_your_image.jpg"; // Replace with the actual file path

            //// Load an image from the specified file
            //using (var image = CvInvoke.Imread(imagePath))
            //{
            //    // Check if the image is valid
            //    if (image.IsEmpty)
            //    {
            //        Console.WriteLine("Error: Unable to load the image.");
            //        return;
            //    }

            //    // Perform license plate detection (you'll need a trained model for this)
            //    // This example only shows basic edge detection
            //    var grayImage = new Mat();
            //    CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

            //    var edges = new Mat();
            //    CvInvoke.Canny(grayImage, edges, 100, 200);

            //    // Find contours in the edges image
            //    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            //    CvInvoke.FindContours(edges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            //    // Initialize the Tesseract engine
            //    using (var engine = new TesseractEngine(@"path_to_tessdata_folder", "eng", EngineMode.Default))
            //    {
            //        engine.DefaultPageSegMode = PageSegMode.SingleBlock;

            //        // Loop through the detected contours
            //        foreach (var contour in contours.ToArrayOfArray())
            //        {
            //            // You may need more advanced filtering here to identify the license plate region

            //            // Create a region of interest (ROI) around the contour
            //            var roi = new Mat(image, CvInvoke.BoundingRectangle(contour));

            //            // Convert the Emgu.CV Mat to a Tesseract.Pix
            //            using (var imgToRecognize = PixConverter.ToPix(roi))
            //            {
            //                using (var page = engine.Process(imgToRecognize))
            //                {
            //                    // Get the recognized text
            //                    string recognizedText = page.GetText();

            //                    // Print the recognized text on the screen
            //                    Console.WriteLine("License Plate: " + recognizedText.Trim());
            //                }
            //            }
            //        }
            //    }

            //    // Display the result image with contours (optional)
            //    CvInvoke.Imshow("License Plate Detection", image);

            //    // Wait for a key press to close the window
            //    CvInvoke.WaitKey(0);
            //} 
            #endregion

            #region 2
            // Specify the path to the image file
            try
            {
                string imagePath = "C://Users//erdem//Documents//test-pic//tessa-test2.jpg"; // Replace with the actual file path

                var tessFodler = "C://Users//erdem//source//repos//plateRecognize//plateRecognize//bin//Debug//net5.0//tessdata";
                var imageFolder = "//2211.jpg";// " 141    //1111.jpg" "//tet-car11111.jpg"; // "//tessa-test2.jpg";
                var saveFolder = "//res//" + "1.jpg";

                // Load an image from the specified file
                using (var image = CvInvoke.Imread(tessFodler+imageFolder))
                {
                    // Check if the image is valid
                    if (image.IsEmpty)
                    {
                        Console.WriteLine("Error: Unable to load the image.");
                        return;
                    }

                    // Perform license plate detection (you'll need a trained model for this)
                    // This example only shows basic edge detection
                    var grayImage = new Mat();
                    CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

                    var edges = new Mat();
                    CvInvoke.Canny(grayImage, edges, 100, 200);

                    // Find contours in the edges image
                    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                    CvInvoke.FindContours(edges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);



                    #region test1 
                    try
                    {
                        using (var engine = new TesseractEngine(tessFodler, "eng", EngineMode.Default))
                        {
                            using (var img = Pix.LoadFromFile(tessFodler + imageFolder))
                            {
                                using (var page = engine.Process(img))
                                {
                                    string text = page.GetText();
                                    Console.WriteLine(text);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    #endregion


                    #region test2 
                    try
                    {
                         //Specify the path to the Tesseract executable (update with your path)
                        
                         //=> degisik version  
                        
                        var tesseractEngine = new TesseractEngine(tessFodler, "eng", EngineMode.Default); 
                        tesseractEngine.SetVariable("TESSDATA_PREFIX", tessFodler);

                        using (var img = Pix.LoadFromFile(tessFodler + imageFolder))
                        {
                            using (var page = tesseractEngine.Process(img))
                            {
                                var text = page.GetText();
                                Console.WriteLine(text);
                            }
                        }
                    }
                    catch ( Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    #endregion

                   


                    try
                    {
                        // Initialize the Tesseract engine @"path_to_tessdata_folder"
                        using (var engine = new TesseractEngine(tessFodler, "eng", EngineMode.Default))
                        {
                            engine.DefaultPageSegMode = PageSegMode.SingleBlock;

                            // Loop through the detected contours
                            foreach (var contour in contours.ToArrayOfArray())
                            {
                                // You may need more advanced filtering here to identify the license plate region

                                // Create a region of interest (ROI) around the contour
                                var roi = new Mat(image, CvInvoke.BoundingRectangle(contour));

                                // Save the ROI as an image file
                                string roiImagePath = tessFodler + saveFolder;  // Specify a file path
                                CvInvoke.Imwrite(roiImagePath, roi);

                                // Load the saved image as a Tesseract.Pix
                                using (var imgToRecognize = Pix.LoadFromFile(roiImagePath))
                                {
                                    using (var page = engine.Process(imgToRecognize))
                                    {
                                        // Get the recognized text
                                        string recognizedText = page.GetText();

                                        // Print the recognized text on the screen
                                        Console.WriteLine("License Plate: " + recognizedText.Trim());
                                    }
                                }
                            }
                        }

                        // Display the result image with contours (optional)
                        CvInvoke.Imshow("License Plate Detection", image);

                        // Wait for a key press to close the window
                        CvInvoke.WaitKey(0);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("test");
            }
            #endregion
        }

         
    }


}



 