namespace Service.Converter.WebApi.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
  
    using Context;
    using Domain.Entities;
    using Service.Converter.WebApi.Domain.Repositories;

    public class AuditRepository : IAuditRepository
    {
        private readonly ConverterContext _context;

        public AuditRepository(ConverterContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.AsNoTracking().OrderBy(x => x.Email);
        }

        public IEnumerable<Audit> GetByUserId(string userName)
        {
            return _context.Audits.AsNoTracking().Where(x => x.UserName == userName).ToList();
        }

        public void Add(Audit audit)
        {
            _context.Audits.Add(audit);
            _context.SaveChanges();
        }

        public void AddAuditEntry(string userName, string requestUrl, string requestData, string responseData)
        {
            var audit = new Audit()
            {
                Id = new Guid(),
                UserName = userName,
                Created = DateTimeOffset.Now,
                ResponseData = responseData,
                RequestData = requestData,
                RequestUrl = requestUrl
            };

            _context.Audits.Add(audit);
            _context.SaveChanges();
        }

        IEnumerable<Audit> IAuditRepository.GetAll()
        {
            return _context.Audits.AsNoTracking().OrderBy(x => x.UserName);
        }
    }
}
