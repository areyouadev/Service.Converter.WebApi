namespace Service.Converter.WebApi.Domain.Handlers
{
    using System;

    using Commands;
    using Entities;
    using Contracts;
    using Repositories;
    using Commands.Audit;
    using Service.Converter.WebApi.Domain.Commands.Contracts;

    public class AuditHandler :
     IHandler<CreateAuditCommand>

    {
        private readonly IAuditRepository _auditRepository;

        public AuditHandler(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public ICommandResult Handle(CreateAuditCommand command)
        {
            var audit = new Audit() { Created  = DateTimeOffset.Now, UserName = command.UserName, Id = Guid.NewGuid(), RequestData = command.RequestData, ResponseData = command.RequestData, RequestUrl = command.RequestUrl };
            
            _auditRepository.Add(audit);
 
            return new GenericCommandResult(true, "Audit entry created successfully", audit);
        }
      
    }
}
