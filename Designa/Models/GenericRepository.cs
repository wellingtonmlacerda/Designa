using Designa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Designa.Models
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private DesignaContext _context;
        protected DbSet<TEntity> _dbSet;
        public GenericRepository(DesignaContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public TEntity CreateNewObject()
        {
            var newInstance = Activator.CreateInstance<TEntity>();
            return newInstance;
        }
        public List<TEntity> CreateNewObjectList()
        {
            var newInstance = Activator.CreateInstance<List<TEntity>>();
            return newInstance;
        }
        public void Add(TEntity model)
        {
           _dbSet.Add(model);
        }
        public void AddRange(IEnumerable<TEntity> model)
        {
            _dbSet.AddRange(model);
        }
        public TEntity? GetId(int id)
        {
            return _dbSet.Find(id);
        }
        public async Task<TEntity?> GetIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<TEntity?> GetIdWithIncludesAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        {
            IQueryable<TEntity> query = _dbSet;
            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate) ?? CreateNewObject();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate) ?? CreateNewObject();
        }
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => _dbSet.Where(predicate));
        }
        public async Task<IEnumerable<TEntity>> GetListWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            return await Task.Run(() => query.Where(predicate));
        }
        public async Task<IEnumerable<TEntity>> GetAllWithIncludes(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            return await query.ToListAsync();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => _dbSet);
        }
        public int Count()
        {
            return _dbSet.Count();
        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
        public bool All(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.All(predicate);
        }
        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
        public async Task UpdateAsync(TEntity objModel)
        {
            _context.Entry(objModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(TEntity objModel)
        {
            _dbSet.Remove(objModel);
            await _context.SaveChangesAsync();
        }
        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
