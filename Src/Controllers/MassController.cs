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
    public class MassController : ControllerBase
    {
        private readonly IMassConverter _massConverter;
        private readonly IAuditRepository _auditRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly string userName = string.Empty;
        private readonly string methodName = string.Empty;

        public MassController(IMassConverter massConverter, IAuditRepository auditRepository, IHttpContextAccessor accessor)
        {
            _massConverter = massConverter;
            _auditRepository = auditRepository;
            _accessor = accessor;
            userName = _accessor.HttpContext.User.Identity.Name;
            methodName = _accessor.HttpContext.Request.Method + " " + _accessor.HttpContext.Request.Path;
        }

        [HttpGet]
        [Route("PoundToKilograms/{poundValue}")]
        public IActionResult ConvertToKilograms(decimal poundValue)
        {
            var response = _massConverter.ConvertPoundToKilograms(poundValue);
            _auditRepository.AddAuditEntry(userName, requestUrl: methodName, poundValue.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture));
            return Ok(response);
        }

        [HttpGet]
        [Route("KilogramToPounds/{kilogramValue}")]
        public IActionResult ConvertToPounds(decimal kilogramValue)
        {
            var response = _massConverter.ConvertKilogramToPounds(kilogramValue);
            _auditRepository.AddAuditEntry(userName, requestUrl: methodName, kilogramValue.ToString(CultureInfo.InvariantCulture),
                response.ToString(CultureInfo.InvariantCulture));
            return Ok(response);
        }
    }
}
