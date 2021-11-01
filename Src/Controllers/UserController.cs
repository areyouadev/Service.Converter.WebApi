namespace Service.Converter.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Domain.Commands;
    using Domain.Entities;
    using Domain.Handlers;
    using Domain.Repositories;
    using Domain.Commands.Users;

    [ApiController]
    [Route("v1/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly UserHandler _handler;
        private readonly IHttpContextAccessor _accessor;
        private readonly string _userName = string.Empty;
        private readonly string _methodName = string.Empty;


        public UsersController(IUserRepository userRepository, UserHandler handler, IAuditRepository auditRepository, IHttpContextAccessor accessor)
        {
            _userRepository = userRepository;
            _auditRepository = auditRepository;
            _handler = handler;
            _accessor = accessor;
            _userName = _accessor.HttpContext.User.Identity.Name;
            _methodName = _accessor.HttpContext.Request.Method +" " + _accessor.HttpContext.Request.Path;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var response = _userRepository.GetAll();
            _auditRepository.AddAuditEntry(_userName, _methodName, "", JsonConvert.SerializeObject(response));
            return Ok(response);


        }

        [HttpGet("{id:Guid}")]
        public ActionResult<IEnumerable<User>> Get(Guid id)
        {
            var response = _userRepository.GetById(id);
            _auditRepository.AddAuditEntry(_userName, _methodName, id.ToString(), JsonConvert.SerializeObject(response));
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<GenericCommandResult> Create([FromBody] CreateCommand command)
        {
            var response = (GenericCommandResult)_handler.Handle(command);
            _auditRepository.AddAuditEntry(string.Empty, _methodName, JsonConvert.SerializeObject(command), JsonConvert.SerializeObject(response));
            return Ok(response);
        }

        [HttpPut]
        public ActionResult<GenericCommandResult> Update([FromBody] UpdateCommand command)
        {
            var response = (GenericCommandResult)_handler.Handle(command);
            _auditRepository.AddAuditEntry(_userName, _methodName, JsonConvert.SerializeObject(command), JsonConvert.SerializeObject(response));
            return Ok(response);
        }

        [Route("changepassowrd")]
        [HttpPut]
        public ActionResult<GenericCommandResult> UpdatePass([FromBody] UpdatePassawordCommand command)
        {
            var response = (GenericCommandResult)_handler.Handle(command);
            _auditRepository.AddAuditEntry(_userName, _methodName, null, JsonConvert.SerializeObject(response));
            return Ok(response);
        }

        [HttpDelete]
        public ActionResult<GenericCommandResult> Delete([FromBody] DeleteCommand command)
        {
            var response = (GenericCommandResult)_handler.Handle(command);
            _auditRepository.AddAuditEntry(_userName, _methodName, JsonConvert.SerializeObject(command), JsonConvert.SerializeObject(response));

            return Ok(response);
        }
    }
}
