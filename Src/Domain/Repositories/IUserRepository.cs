namespace Service.Converter.WebApi.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        User GetByEmail(string email);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
