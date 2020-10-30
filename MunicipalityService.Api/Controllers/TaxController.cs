using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MunicipalityService.Api.Filters;
using MunicipalityService.Business.Interfaces;
using MunicipalityService.Models;

namespace MunicipalityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxManager iTaxManager;

        private readonly ILogger<TaxController> iLogger;
        public TaxController(ITaxManager iTaxManager,
            ILogger<TaxController> iLogger)
        {
            this.iTaxManager = iTaxManager;
            this.iLogger = iLogger;
        }

        [HttpGet]       
        public IActionResult Get()
        {
            iLogger.LogInformation("Execution Started - GetALLTaxDetails");
            
            var response = iTaxManager.GetALLTaxDetails();

            iLogger.LogInformation("Execution Ended - GetALLTaxDetails");

            return Ok(response);
        }

        [HttpPost]
        [Route("GetTaxDetails")]
        [ServiceFilter(typeof(ValidateModelStateAttribute))]
        public IActionResult GetTaxDetails(Municipality municipality)
        {
            if (municipality == null)
            {
                return BadRequest();
            }            

            iLogger.LogInformation("Execution Started - GetTaxDetails Search API");

            var response = iTaxManager.GetTaxDetails(municipality.Name, municipality.Date);

            iLogger.LogInformation("Execution Ended - GetTaxDetails Search API");

            return Ok(response);
        }

        [HttpPost]
        [Route("Save")]
        [ServiceFilter(typeof(ValidateModelStateAttribute))]
        public IActionResult Save(Municipality municipality)
        {
            if (municipality == null)
            {
                return BadRequest();
            }
            iLogger.LogInformation("Execution Started - Save Tax Details");
            
            var response = iTaxManager.Save(municipality);

            iLogger.LogInformation("Execution Ended - Save Tax Details");

            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        [ServiceFilter(typeof(ValidateModelStateAttribute))]
        public IActionResult Put(Municipality municipality)
        {
            if (municipality == null)
            {
                return BadRequest();
            }

            iLogger.LogInformation("Execution Started - Update Tax Details");

            var response = iTaxManager.Update(municipality);

            iLogger.LogInformation("Execution Ended - Update Tax Details");

            return Ok(response);
        }

    }
}
