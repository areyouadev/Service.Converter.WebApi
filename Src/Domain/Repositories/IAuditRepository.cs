namespace Service.Converter.WebApi.Domain.Repositories
{
    using System;
    using System.Collections.Generic;

    using Entities;

    public interface IAuditRepository
    {
        IEnumerable<Audit> GetAll();
        IEnumerable<Audit> GetByUserId(string userName);
        void Add(Audit audit);
        void AddAuditEntry(string userName, string requestUrl, string requestData, string responseData);
    }
}
