using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Repositories.Abstractions;

namespace Repositories.NHibernate
{
    public interface INHibernateRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        Task SaveOrUpdateAsync(TEntity entity, CancellationToken cancellation);

        Task SaveOrUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellation);

        Task UpdateAsync(TEntity entity, CancellationToken cancellation);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation);
    }
}
