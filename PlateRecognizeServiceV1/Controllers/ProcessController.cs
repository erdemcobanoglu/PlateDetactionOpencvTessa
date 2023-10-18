using ImageProcessLibrary.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using plateRecognize;

namespace PlateRecognizeServiceV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage([FromBody] Base64ImageModel model)
        {
            try
            {
                model.Base64Data = model.Base64Data; // new TestClass().TestImage();

                if (model == null || string.IsNullOrEmpty(model.Base64Data))
                {
                    return BadRequest("Invalid base64 image data.");
                }
                 
                var image = Base64ToImageConverter.ConvertBase64ToImage(model.Base64Data);
                var imageFolderSavePath = "\\ImageSaveProcess";
                // save image
                var ImageName = Base64ToImageConverter.SaveImageToFile(image, ProjectPathHelper.GetProjectDirectory()+imageFolderSavePath);
                  
                return Ok(ImageName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("ImagetoPlate")]
        public async Task<IActionResult> ConvertImagetoPlate([FromBody] ImageTextModel model)
        {
            try
            {
                // burda base64 olarak alicaz datayi 2) data yi isleme sokup kayit edicez.ConvertBase64ToImage and SaveFile kayittan sonra Imagepath donup onu alicaz ve isleme baslicaz. 
                 

                // burayida config"den okuyup bir alt dosyada halletmek gerekir
                string tessDataFolderName = "//tessdata";
                  
                // burayida config"den okuyup bir alt dosyada halletmek gerekir
                // burayida configden okumaliyiz bence
                string roiSaveFolder = "\\res\\" + "1.jpg";



                var result = model.Base64Data.ToString().StartProcess(tessDataFolderName, roiSaveFolder);
                 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
