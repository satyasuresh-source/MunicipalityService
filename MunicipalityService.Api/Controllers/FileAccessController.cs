using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MunicipalityService.Api.Filters;
using MunicipalityService.Business.Interfaces;
using MunicipalityService.Models;

namespace MunicipalityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileAccessController : ControllerBase
    {

        private readonly IFileAccessManager iFileAccessManager;
        private readonly ILogger<FileAccessController> iLogger;
        public FileAccessController(IFileAccessManager iFileAccessManager,
            ILogger<FileAccessController> iLogger)
        {
            this.iFileAccessManager = iFileAccessManager;
            this.iLogger = iLogger;
        }

        [HttpPost]
        [Route("DocumentUpload")]
        [ServiceFilter(typeof(ValidateModelStateAttribute))]
        public IActionResult DocumentUpload(FileAccessRequest fileAccessRequest)
        {           
            if (fileAccessRequest == null)
            {
                return BadRequest();
            }

            iLogger.LogInformation("Execution Started - Document Upload");

            var response = iFileAccessManager.DocumentUpload(fileAccessRequest);

            iLogger.LogInformation("Execution Ended - Document Upload");

            return Ok(response);
        }
    }
}
