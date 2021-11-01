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
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureConverter  _temperatureConverter;
        private readonly IAuditRepository _auditRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly string _userName = string.Empty;
        private readonly string _methodName = string.Empty;

        public TemperatureController(ITemperatureConverter temperatureConverter, IAuditRepository auditRepository, IHttpContextAccessor accessor)
        {
            _temperatureConverter = temperatureConverter;
            _auditRepository = auditRepository;
            _accessor = accessor;
            _userName = _accessor.HttpContext.User.Identity.Name;
            _methodName = _accessor.HttpContext.Request.Method + " " + _accessor.HttpContext.Request.Path;
        }

        [HttpGet]
        [Route("FahrenheitToCelsius/{fahrenheitValue}")]
        public IActionResult FahrenheitToCelsius(decimal fahrenheitValue)
        {
            var response = _temperatureConverter.ConvertFahrenheitToCelsius(fahrenheitValue);
            _auditRepository.AddAuditEntry(_userName,  _methodName, fahrenheitValue.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture));
            return Ok(response);
        }

        [HttpGet]
        [Route("CelsiusToFahrenheit/{celsiusValue}")]
        public IActionResult CelsiusToFahrenheit(decimal celsiusValue)
        {
            var response = _temperatureConverter.ConvertCelsiusToFahrenheit(celsiusValue);
            _auditRepository.AddAuditEntry(_userName,  _methodName, celsiusValue.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture));
            return Ok(response);
        }
    }
}
