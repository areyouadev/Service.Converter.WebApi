namespace Service.Converter.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("v1/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Healthy");
        }
    }
}
