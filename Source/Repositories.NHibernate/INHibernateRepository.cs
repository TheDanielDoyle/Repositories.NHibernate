using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Repositories.Abstractions;

namespace Repositories.NHibernate
{
    public interface INHibernateRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        Task SaveOrUpdateAsync(TEntity entity, CancellationToken cancellation = default);

        Task SaveOrUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellation = default);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default);
    }
}
