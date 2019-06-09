using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductCatalog.Domain.Business;

namespace ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<PicController> _logger;
        private readonly IPicBusiness _picBusiness;

        public PicController(IHostingEnvironment environment, [FromServices] ILogger<PicController> logger, [FromServices] IPicBusiness picBusiness)
        {
            _environment = environment;
            _logger = logger;
            _picBusiness = picBusiness;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetImageById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var image = _picBusiness.GetImageById(id, _environment.WebRootPath);

                if (image == null)
                {
                    return NotFound();
                }

                return File(image, "image/jpg");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in GetImageById");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}