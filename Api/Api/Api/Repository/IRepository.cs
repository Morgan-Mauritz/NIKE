using Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<TEntity> Get(string username);
        Task<User> GetByApiKey(string apiKey);
        Task<User> GetByUserId(long userId);
        Task<User> GetByLogin(string email, byte[] password);
        Task UpdateUser();
        Task RemoveUser(User user);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
