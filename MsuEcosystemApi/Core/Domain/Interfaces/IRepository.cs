using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> expression);
        T Get(string id);
        void Delete(string id);
        void Update(T entity);
        void Create(T entity);
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(string id);
    }
}
