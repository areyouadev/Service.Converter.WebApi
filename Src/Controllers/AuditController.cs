namespace Service.Converter.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Domain.Commands;
    using Domain.Entities;
    using Domain.Handlers;
    using Domain.Repositories;
    using Domain.Commands.Audit;

    [ApiController]
    [Route("v1/[controller]")]
    [Authorize]
    public class AuditController : ControllerBase
    {
        private readonly IAuditRepository _auditRepository;
        private readonly AuditHandler _handler;

        public AuditController(IAuditRepository auditRepository, AuditHandler handler)
        {
            _auditRepository = auditRepository;
            _handler = handler;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Audit>> GetAll()
        {
            return Ok(_auditRepository.GetAll());
        }

        [HttpGet("{userName}")]
        public ActionResult<IEnumerable<User>> GetByUserId(string userName)
        {
            return Ok(_auditRepository.GetByUserId(userName));
        }

        [HttpPost]
        public ActionResult<GenericCommandResult> Create([FromBody] CreateAuditCommand command)
        {
            return Ok((GenericCommandResult)_handler.Handle(command));
        }
    }
}
