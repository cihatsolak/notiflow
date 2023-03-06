namespace Notiflow.Lib.Database.Infrastructure.Extensions
{
    /// <summary>
    /// Extension method to manage queries according to isolation levels
    /// <see cref="TransactionScope"/>
    /// <see cref="IsolationLevel"/>
    /// </summary>
    public static class NoLockExtensions
    {
        /// <summary>
        /// Any method with isolation level read uncommitted
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="query">entity queryable</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>query result</returns>
        public static async ValueTask<bool> ToAnyWithNoLockAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
        {
            bool result;
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                          new TransactionOptions()
                                          {
                                              IsolationLevel = IsolationLevel.ReadUncommitted
                                          },
                                          TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.AnyAsync(cancellationToken);
                transactionScope.Complete();
            }

            return result;
        }

        /// <summary>
        /// ToArray method with isolation level read uncommitted 
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="query">entity queryable</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>query result</returns>
        public static async Task<TEntity[]> ToArrayWithNoLockAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
        {
            TEntity[] result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.ToArrayAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }

        /// <summary>
        /// ToList method with isolation level read uncommitted 
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="query">entity queryable</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>query result</returns>
        public static async Task<List<TEntity>> ToListWithNoLockAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
        {
            List<TEntity> result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.ToListAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }


        /// <summary>
        /// FirstOrDefault method with isolation level read uncommitted 
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="query">entity queryable</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>query result</returns>
        public static async Task<TEntity> ToFirstOrDefaultWithNoLockAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
        {
            TEntity result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.FirstOrDefaultAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }

        /// <summary>
        /// First method with isolation level read uncommitted 
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="query">entity queryable</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>query result</returns>
        public static async Task<TEntity> ToFirstWithNoLockAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
        {
            TEntity result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.FirstAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }

        /// <summary>
        /// Single method with isolation level read uncommitted 
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="query">entity queryable</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>query result</returns>
        public static async Task<TEntity> ToSingleWithNoLockAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class, IEntity, new()
        {
            TEntity result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.SingleAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }
    }
}
