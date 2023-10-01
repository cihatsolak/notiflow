namespace Puzzle.Lib.Database.Infrastructure.Extensions;

public static class QueryableExtensions
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

    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}
