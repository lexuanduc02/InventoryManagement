using InventoryManagement.Domains.Contractors;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryManagement.Repositories.Contractors
{
    public interface IRepository<TEntity, in TKey, Context>
        where TEntity : IEntity<TKey>
        where Context : DbContext
    {
        Task<TEntity?> GetById(TKey id, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>?> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> FirstOrDefaultByAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);
    }
}
