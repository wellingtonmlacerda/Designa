using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Designa.Models
{
    public interface IGenericRepository<T> where T : class
    {
        T CreateNewObject();
        List<T> CreateNewObjectList();
        void Add(T objModel);
        void AddRange(IEnumerable<T> objModel);
        T? GetId(int id);
        Task<T?> GetIdAsync(int id);
        public Task<T?> GetIdWithIncludesAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object?>> include);
        T Get(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListWithIncludesAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object?>> include);
        Task<IEnumerable<T>> GetAllWithIncludes(Func<IQueryable<T>, IIncludableQueryable<T, object?>> include);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        int Count();
        Task<int> CountAsync();
        bool Any(Expression<Func<T, bool>> predicate);
        bool All(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T objModel);
        Task RemoveAsync(T objModel);
        Task<int> SaveAsync();
        void Dispose();
    }
}
