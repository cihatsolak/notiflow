namespace Notiflow.Lib.Database.Infrastructure.Extensions
{
    /// <summary>
    /// MS SQL with(nolock) extensions
    /// <seealso cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.transactions.isolationlevel?view=net-6.0"/>
    /// </summary>
    public static class NoLockExtensions
    {
        /// <summary>
        /// Any with no lock async
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="query">entity queryable</param>
        /// <param name="cancellationToken">default cancellation token</param>
        /// <returns>type of boolean</returns>
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
        /// To array with no lock asynchronous 
        /// </summary>
        /// <typeparam name="TEntity">entity</typeparam>
        /// <param name="query">entity query</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>type of entity array</returns>
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
        /// To list with no lock asynchronous
        /// </summary>
        /// <typeparam name="TEntity">entity</typeparam>
        /// <param name="query">entity query</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>type of entity list</returns>
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
        /// To first or default with no lock asynchronous
        /// </summary>
        /// <typeparam name="TEntity">entity</typeparam>
        /// <param name="query">entity query</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>type of entity</returns>
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
        /// To first with no lock asynchronous
        /// </summary>
        /// <typeparam name="TEntity">entity</typeparam>
        /// <param name="query">entity query</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>type of entity</returns>
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
        /// To signle with no lock asynchronous
        /// </summary>
        /// <typeparam name="TEntity">entity</typeparam>
        /// <param name="query">entity query</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>type of entity</returns>
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
