namespace Notiflow.Lib.Database.Infrastructure.Extensions
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Query with dynamic include
        /// </summary>
        /// <typeparam name="TEntity">entity</typeparam>
        /// <param name="context">dbContext</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns>constructed query with include properties</returns>
        /// <exception cref="ArgumentNullException">when query value is empty or not</exception>
        public static IQueryable<TEntity> Includes<TEntity>(this IQueryable<TEntity> query, params string[] includeProperties) where TEntity : class, IEntity, new()
        {
            ArgumentNullException.ThrowIfNull(query);
            ArgumentNullException.ThrowIfNull(includeProperties);

            return includeProperties.Aggregate(query, EntityFrameworkQueryableExtensions.Include);
        }

        /// <summary>
        /// Bulk add to collection type
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="collection">type of collection interface</param>
        /// <param name="items">type of ienumerable interface</param>
        public static void ToAddRange<T>(this ICollection<T> collection, IEnumerable<T> items) where T : class, new()
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(items);

            foreach (var item in items)
                collection.Add(item);
        }

        /// <summary>
        /// Bulk add to list type
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="collection">type of collection interface</param>
        /// <param name="items">type of ilist interface</param>
        public static void ToAddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(items);

            foreach (var item in items)
                collection.Add(item);
        }
    }
}
