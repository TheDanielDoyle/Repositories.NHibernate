using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using Repositories.Abstractions;

namespace Repositories.NHibernate
{
    public abstract class Repository<TEntity, TId> : INHibernateRepository<TEntity, TId>
        where TEntity : class
    {
        private readonly ISession _session;

        protected Repository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellation = default)
        {
            return _session.SaveAsync(entity, cancellation);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default)
        {
            foreach (TEntity entity in entities)
            {
                await _session.SaveAsync(entity, cancellation).ConfigureAwait(false);
            }
        }

        public Task<long> CountAsync(CancellationToken cancellation = default)
        {
            return _session.QueryOver<TEntity>().RowCountInt64Async(cancellation);
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default)
        {
            return _session.QueryOver<TEntity>().Where(predicate).RowCountInt64Async(cancellation);
        }

        public Task<long> CountAsync(IRepositoryQuery<TEntity> query, CancellationToken cancellation = default)
        {
            return _session.QueryOver<TEntity>().Where(query.GetQuery()).RowCountInt64Async(cancellation);
        }

        public abstract Task<TEntity> FindByIdAsync(TId id, CancellationToken cancellation = default);

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default)
        {
            return await _session.QueryOver<TEntity>().Where(predicate).ListAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, CancellationToken cancellation = new CancellationToken())
        {
            return await _session.QueryOver<TEntity>().Where(predicate).Skip(skip).Take(take).ListAsync(cancellation).ConfigureAwait(false);;
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(IRepositoryQuery<TEntity> query, CancellationToken cancellation = default)
        {
            return await _session.QueryOver<TEntity>().Where(query.GetQuery()).ListAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(IRepositoryQuery<TEntity> query, int skip, int take, CancellationToken cancellation = new CancellationToken())
        {
            return await _session.QueryOver<TEntity>().Where(query.GetQuery()).Skip(skip).Take(take).ListAsync(cancellation).ConfigureAwait(false);
        }

        public Task RemoveAsync(TEntity entity, CancellationToken cancellation = default)
        {
            return _session.DeleteAsync(entity, cancellation);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation = default)
        {
            foreach (TEntity entity in entities)
            {
                await _session.DeleteAsync(entity, cancellation).ConfigureAwait(false);
            }
        }

        public Task SaveOrUpdateAsync(TEntity entity, CancellationToken cancellation)
        {
            return _session.SaveOrUpdateAsync(entity, cancellation);
        }

        public async Task SaveOrUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellation)
        {
            foreach (TEntity entity in entities)
            {
                await _session.SaveOrUpdateAsync(entity, cancellation).ConfigureAwait(false);
            }
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellation)
        {
            return _session.UpdateAsync(entity, cancellation);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellation)
        {
            foreach (TEntity entity in entities)
            {
                await _session.UpdateAsync(entity, cancellation).ConfigureAwait(false);
            }
        }
    }
}
