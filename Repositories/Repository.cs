using InventoryManagement.Domains.Contractors;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryManagement.Repositories
{
    public abstract class Repository<TEntity, TKey, Context>
        where TEntity : class, IEntity<TKey>, new()
        where Context : DbContext
    {
        protected readonly Context _context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(Context context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetById(TKey id, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = await FindByAsync(o => o.Id != null && o.Id.Equals(id), includes);
            return result.FirstOrDefault();
        }

        public async Task<List<TEntity>?> FindByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            var result = await query.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<TEntity?> FirstOrDefaultByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            var result = await query.Where(predicate).FirstOrDefaultAsync();
            return result;
        }
    }
}
