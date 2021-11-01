namespace Service.Converter.WebApi.Controllers
{
    using System.Globalization;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Domain.Repositories;

    [ApiController]
    [Authorize]
    [Route("v1/[controller]")]
    public class LengthController : ControllerBase
    {
        private readonly ILengthConverter _lengthConverter;
        private readonly IAuditRepository _auditRepository;
        private readonly IHttpContextAccessor _accessor;
        private string userName = string.Empty;
        private readonly string methodName = string.Empty;

        public LengthController(ILengthConverter lengthConverter, IAuditRepository auditRepository, IHttpContextAccessor accessor)
        {
            _lengthConverter = lengthConverter;
            _auditRepository = auditRepository;
            _accessor = accessor;
            userName = _accessor.HttpContext.User.Identity.Name;
            methodName = _accessor.HttpContext.Request.Method + " " + _accessor.HttpContext.Request.Path;
        }

        [HttpGet]
        [Route("MilesToKilometers/{mileValue}")]
        public IActionResult MilesToKilometers(decimal mileValue)
        {
            var response = _lengthConverter.ConvertMilesToKilometers(mileValue);
            _auditRepository.AddAuditEntry(userName, methodName, mileValue.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture));
            return Ok(response);
        }

        [HttpGet]
        [Route("KilometersToMiles/{kilometerValue}")]
        public IActionResult KilometersToMiles(decimal kilometerValue)
        {
            var response = _lengthConverter.ConvertKilometersToMiles(kilometerValue);
            _auditRepository.AddAuditEntry(userName, methodName, kilometerValue.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture));
            return Ok(response);
        }
    }
}
