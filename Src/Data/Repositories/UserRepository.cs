namespace Service.Converter.WebApi.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
  
    using Context;
    using Domain.Entities;
    using Service.Converter.WebApi.Domain.Repositories;

    public class UserRepository : IUserRepository
    {
        private readonly ConverterContext _context;

        public UserRepository(ConverterContext context)
        {
            _context = context;
        }


        public IEnumerable<User> GetAll()
        {
            return _context.Users.AsNoTracking().OrderBy(x => x.Email);
        }

        public User GetById(Guid id)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
        }

        public void Add(User user)
        {
            var result = _context.Users.Any(x => x.Email == user.Email);
            if (!result)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }
    }
}
