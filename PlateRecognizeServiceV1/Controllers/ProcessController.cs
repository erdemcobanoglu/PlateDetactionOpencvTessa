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

                //var imageFolderSavePath = "\\ImageSaveProcess"; 
                var getFolderInformation = new LocalInformationGetter().CheckAndGetMemoryData("Localinfo.json"); //new LocalInformationGetter

                // save image
                var ImageName = Base64ToImageConverter.SaveImageToFile(image, ProjectPathHelper.GetProjectDirectory() + getFolderInformation.ImageFolderSavePath);

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
                var getFolderInformation = new LocalInformationGetter().CheckAndGetMemoryData("Localinfo.json"); //new LocalInformationGetter().ReadJsonConfig("Localinfo.json");

                var result = model.Base64Data.ToString().StartProcess(getFolderInformation.TessDataFolderName, $"{getFolderInformation.RoiSaveFolder}{getFolderInformation.TempPictureName}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
