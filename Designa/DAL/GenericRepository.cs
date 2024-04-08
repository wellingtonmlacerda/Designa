using Designa.Data;
using Designa.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Designa.DAL
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
        public TEntity CreateNew()
        {
            var teste = Activator.CreateInstance<TEntity>();
            return teste;
        }
        public void Add(TEntity model)
        {
            _dbSet.Add(model);
            _context.SaveChanges();
        }
        public void AddRange(IEnumerable<TEntity> model)
        {
            _dbSet.AddRange(model);
            _context.SaveChanges();
        }
        public TEntity? GetId(int id)
        {
            return _dbSet.Find(id);
        }
        public async Task<TEntity?> GetIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<TEntity?> GetIdWithIncludesAsync(int id, params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Encontrar a entidade pelo ID
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where<TEntity>(predicate).ToList();
        }
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => _dbSet.Where<TEntity>(predicate));
        }
        public async Task<IEnumerable<TEntity>> GetAllWithIncludes(params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

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
        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
        public void Update(TEntity objModel)
        {
            _context.Entry(objModel).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity objModel)
        {
            _dbSet.Remove(objModel);
            _context.SaveChanges();
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
