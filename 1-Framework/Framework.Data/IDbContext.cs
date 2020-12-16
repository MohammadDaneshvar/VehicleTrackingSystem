using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Data
{
    public interface IDbContext
    {
        Task BeginAsync(CancellationToken cancellationToken = default);
        void Commit();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
        Task<TEntity> FindAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class;
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;
        void Remove<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class;
        Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;
        IQueryable<T> Query<T>() where T : class;
    }
}
