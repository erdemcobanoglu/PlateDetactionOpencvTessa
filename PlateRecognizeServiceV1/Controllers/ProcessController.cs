using ImageProcessLibrary.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace PlateRecognizeServiceV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromBody] Base64ImageModel model)
        {
            try
            { 
                model.Base64Data = new TestClass().TestImage();

                if (model == null || string.IsNullOrEmpty(model.Base64Data))
                {
                    return BadRequest("Invalid base64 image data.");
                }

                // Decode the base64 image data
                //byte[] imageBytes = Convert.FromBase64String(model.Base64Data);

                var image = Base64ToImageConverter.ConvertBase64ToImage(model.Base64Data);
                var imageFolderSavePath = "\\ImageSaveProcess";
                // save image
                var ImageName = Base64ToImageConverter.SaveImageToFile(image, ProjectPathHelper.GetProjectDirectory()+imageFolderSavePath);


                // You can process the image bytes here or save them to a file, etc.
                // Example: File.WriteAllBytes("path_to_save_image.png", imageBytes);

                return Ok("Image uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
