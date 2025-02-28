﻿using Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly NIKEContext _context;
        public UserRepository(NIKEContext context)
        {
            _context = context;
        }
        public async Task Add(User entity)
        { 
           await _context.AddAsync(entity);

           await _context.SaveChangesAsync();
            
        }

        public void AddRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(string username)
        {
            return await _context.Users.FirstOrDefaultAsync( x => x.Username == username);
        }
        public async Task<User> GetByLogin(string email, byte[] password)
        {
            return await _context.Users.AsNoTracking().Where(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();
        }
        public async Task<User> GetByApiKey(string apiKey)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.ApiKey == apiKey);
        }
        public async Task<User> GetByUserId(long userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }
        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUser(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
