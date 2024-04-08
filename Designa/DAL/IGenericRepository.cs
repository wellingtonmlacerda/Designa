using Designa.Models;
using System.Linq.Expressions;

namespace Designa.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        public T CreateNew();
        void Add(T objModel);
        void AddRange(IEnumerable<T> objModel);
        T? GetId(int id);
        Task<T?> GetIdAsync(int id);
        public Task<T?> GetIdWithIncludesAsync(int id, params Expression<Func<T, object?>>[] includes);
        T? Get(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllWithIncludes(params Expression<Func<T, object?>>[] includes);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        int Count();
        Task<int> CountAsync();
        void Update(T objModel);
        void Remove(T objModel);
        Task<int> SaveAsync();
        void Dispose();
    }
}
