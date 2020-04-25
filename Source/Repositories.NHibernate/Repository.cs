using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Repositories.Abstractions;

namespace Repositories.NHibernate
{
    public abstract class Repository<TEntity, TId> : INHibernateRepository<TEntity, TId>
        where TEntity : class
    {
        private readonly ISession session;

        protected Repository(ISession session)
        {
            this.session = session;
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellation = default)
        {
            return this.session.SaveAsync(entity, cancellation);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default)
        {
            foreach (TEntity entity in entities)
            {
                await this.session.SaveAsync(entity, cancellation).ConfigureAwait(false);
            }
        }

        public Task<long> CountAsync(CancellationToken cancellation = default)
        {
            return this.session.QueryOver<TEntity>().RowCountInt64Async(cancellation);
        }

        public Task<long> CountAsync(IRepositoryQuery<TEntity> query, CancellationToken cancellation = default)
        {
            return this.session.QueryOver<TEntity>().Where(query.GetQuery()).RowCountInt64Async(cancellation);
        }

        public abstract Task<TEntity> FindByIdAsync(TId id, CancellationToken cancellation = default);

        public async Task<IEnumerable<TEntity>> QueryAsync(IRepositoryQuery<TEntity> query, CancellationToken cancellation = default)
        {
            return await query.Hydrate(Hydrate(this.session.Query<TEntity>()).Where(query.GetQuery())).ToListAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<TEntity> QuerySingleAsync(IRepositoryQuery<TEntity> query, CancellationToken cancellation = default)
        {
            return await query.Hydrate(Hydrate(this.session.Query<TEntity>()).Where(query.GetQuery())).FirstOrDefaultAsync(cancellation).ConfigureAwait(false);
        }

        public Task RemoveAsync(TEntity entity, CancellationToken cancellation = default)
        {
            return this.session.DeleteAsync(entity, cancellation);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default)
        {
            foreach (TEntity entity in entities)
            {
                await this.session.DeleteAsync(entity, cancellation).ConfigureAwait(false);
            }
        }

        public Task SaveOrUpdateAsync(TEntity entity, CancellationToken cancellation = default)
        {
            return this.session.SaveOrUpdateAsync(entity, cancellation);
        }

        public async Task SaveOrUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default)
        {
            foreach (TEntity entity in entities)
            {
                await this.session.SaveOrUpdateAsync(entity, cancellation).ConfigureAwait(false);
            }
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellation = default)
        {
            return this.session.UpdateAsync(entity, cancellation);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default)
        {
            foreach (TEntity entity in entities)
            {
                await this.session.UpdateAsync(entity, cancellation).ConfigureAwait(false);
            }
        }

        protected virtual IQueryable<TEntity> Hydrate(IQueryable<TEntity> queryable)
        {
            return queryable;
        }
    }
}
