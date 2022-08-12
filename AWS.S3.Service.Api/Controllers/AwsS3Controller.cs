using AWS.S3.Service.Api.Configs;
using AWS.S3.Service.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AWS.S3.Service.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AwsS3Controller : Controller
    {
        private readonly IAppConfiguration appConfiguration;
        private IAws3Services awsS3Services;

        public AwsS3Controller(IAppConfiguration appConfiguration,IAws3Services aws3Services)
        {
            appConfiguration = appConfiguration;
            awsS3Services = aws3Services;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        // GET: AwsS3Controller
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadDocumentToS3(IFormFile file)
        {
            try
            {
                if (file is null || file.Length <= 0)
                    return StatusCode((int)HttpStatusCode.BadRequest, "file is required to upload");
                //return ReturnMessage("file is required to upload", (int)HttpStatusCode.BadRequest);

                awsS3Services = new Aws3Services(appConfiguration.AwsAccessKey, appConfiguration.AwsSecretAccessKey, appConfiguration.AwsSessionToken, appConfiguration.Region, appConfiguration.BucketName);

                var result = awsS3Services.UploadFileAsync(file);
                return StatusCode((int)HttpStatusCode.Created, string.Empty);
                //return ReturnMessage(string.Empty, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

                //return ReturnMessage(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{documentName}")]
        public IActionResult GetDocumentFromS3(string documentName)
        {
            try
            {
                if (string.IsNullOrEmpty(documentName))
                    return Content($"The 'documentName' parameter is required: {(int)HttpStatusCode.BadRequest}");

                awsS3Services = new Aws3Services(appConfiguration.AwsAccessKey, appConfiguration.AwsSecretAccessKey, appConfiguration.AwsSessionToken, appConfiguration.Region, appConfiguration.BucketName);

                var document = awsS3Services.DownloadFileAsync(documentName).Result;

                return File(document, "application/octet-stream", documentName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("{documentName}")]
        public IActionResult DeletetDocumentFromS3(string documentName)
        {
            try
            {
                if (string.IsNullOrEmpty(documentName))
                    return StatusCode((int)HttpStatusCode.BadRequest, "The 'documentName' parameter is required");
                //return ReturnMessage("The 'documentName' parameter is required", (int)HttpStatusCode.BadRequest);

                awsS3Services = new Aws3Services(appConfiguration.AwsAccessKey, appConfiguration.AwsSecretAccessKey, appConfiguration.AwsSessionToken, appConfiguration.Region, appConfiguration.BucketName);

                awsS3Services.DeleteFileAsync(documentName);
                return StatusCode(Int32.Parse(documentName), string.Format("The document '{0}' is deleted successfully"));

                //return ReturnMessage(string.Format("The document '{0}' is deleted successfully", documentName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
