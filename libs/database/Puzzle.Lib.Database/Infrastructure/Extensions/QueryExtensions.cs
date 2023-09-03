namespace Puzzle.Lib.Database.Infrastructure.Extensions;

public static class QueryExtensions
{
    /// <summary>
    /// Query with dynamic include
    /// </summary>
    /// <typeparam name="TEntity">entity</typeparam>
    /// <param name="includeProperties">database entities to associate with</param>
    /// <returns>associates the query with the related entities and returns the query</returns>
    /// <exception cref="ArgumentNullException">throw when query is null</exception>
    /// <exception cref="ArgumentNullException">throw when include properties is null</exception>
    public static IQueryable<TEntity> Includes<TEntity>(this IQueryable<TEntity> query, params string[] includeProperties) where TEntity : class, new()
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(includeProperties);

        return includeProperties.Aggregate(query, EntityFrameworkQueryableExtensions.Include);
    }

    /// <summary>
    /// Bulk add to collection type
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <param name="collection">list of collections to add</param>
    /// <param name="items">list to add to collection</param>
    /// <exception cref="ArgumentNullException">throw when collection is null</exception>
    /// <exception cref="ArgumentNullException">throw when items is null</exception>
    public static void ToAddRange<TModel>(this ICollection<TModel> collection, IEnumerable<TModel> items) where TModel : class, new()
    {
        ArgumentNullException.ThrowIfNull(collection);
        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
            collection.Add(item);
    }

    /// <summary>
    /// Bulk add to list type
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <param name="list">list of list to add</param>
    /// <param name="items">list to add to list</param>
    /// <exception cref="ArgumentNullException">throw when list is null</exception>
    /// <exception cref="ArgumentNullException">throw when items is null</exception>
    public static void ToAddRange<TModel>(this IList<TModel> list, IEnumerable<TModel> items)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
            list.Add(item);
    }
}
