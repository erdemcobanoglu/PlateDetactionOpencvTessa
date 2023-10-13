using ImageProcessLibrary.Helper;
using Microsoft.AspNetCore.Mvc;
using plateRecognize;
using plateRecognize.Helper;

namespace PlateRecognizeServiceV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            string imagePath = "C://Users//erdem//Documents//test-pic//tessa-test2.jpg";
            string tessFodler = "C://Users//erdem//source//repos//plateRecognize//plateRecognize//bin//Debug//net5.0//tessdata";
            string imageFolder = "//tet-car11111.jpg"; // "//22111111.jpg"; // calisan ornek "//2211.jpg";  //tet-car11111.jpg
            string saveFolder = "//res//" + "1.jpg";
            try
            { 
                //var avvv = new LetterCounter();
                imagePath.StartProcess(tessFodler, imageFolder, saveFolder);
            }
            catch (Exception ex)
            {

            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpGet(Name = "GetProcess")]
        //public IEnumerable<WeatherForecast> Help()
        //{
        //    string imagePath = "C://Users//erdem//Documents//test-pic//tessa-test2.jpg";
        //    string tessFodler = "C://Users//erdem//source//repos//plateRecognize//plateRecognize//bin//Debug//net5.0//tessdata";
        //    string imageFolder = "//tet-car11111.jpg"; // "//22111111.jpg"; // calisan ornek "//2211.jpg";  //tet-car11111.jpg
        //    string saveFolder = "//res//" + "1.jpg";

        //    try
        //    {
        //        //var avvv = new LetterCounter();
        //        //imagePath.StartProcess(tessFodler, imageFolder, saveFolder);
        //    }
        //    catch (Exception ex)
        //    {
                 
        //    }

        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        //[HttpPost("upload")]
        //public IActionResult UploadBitmap([FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("No file uploaded.");
        //    }

        //    if (file.ContentType != "image/bmp")
        //    {
        //        return BadRequest("Only BMP images are supported.");
        //    }

        //    using (var stream = new MemoryStream())
        //    {
        //        file.CopyTo(stream);
        //        // You can process the bitmap data here or save it to a database, etc.
        //        // Example: var bitmapBytes = stream.ToArray();
        //    }

        //    return Ok("Bitmap uploaded successfully.");
        //}

        //public string StartProcces(int ProcessNumber)
        //{
        //    string imagePath = "C://Users//erdem//Documents//test-pic//tessa-test2.jpg";
        //    string tessFodler = "C://Users//erdem//source//repos//plateRecognize//plateRecognize//bin//Debug//net5.0//tessdata";
        //    string imageFolder = "//tet-car11111.jpg"; // "//22111111.jpg"; // calisan ornek "//2211.jpg";  //tet-car11111.jpg
        //    string saveFolder = "//res//" + "1.jpg";

        //    imagePath.StartProcess(tessFodler, imageFolder, saveFolder);

        //    //using (TessaV2Extension stream = new TessaV2Extension())
        //    //{ 
        //    //    // You can process the bitmap data here or save it to a database, etc.
        //    //    // Example: var bitmapBytes = stream.ToArray();
        //    //}

        //    return "Bitmap uploaded successfully.";
        //}


    }
}