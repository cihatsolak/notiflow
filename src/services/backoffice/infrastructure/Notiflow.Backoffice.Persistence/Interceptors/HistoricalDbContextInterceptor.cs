namespace Notiflow.Backoffice.Persistence.Interceptors;

public sealed class HistoricalDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<EntityEntry<IBaseHistoricalEntity>>> Behaviors = new()
    {
        {EntityState.Added,AddBehavior},
        {EntityState.Modified,UpdateBehavior }
    };

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        var baseHistoricalEntities = eventData.Context.ChangeTracker.Entries<IBaseHistoricalEntity>();
        foreach (var baseHistoricalEntity in baseHistoricalEntities)
        {
            Behaviors[baseHistoricalEntity.State](baseHistoricalEntity);
        }

        var baseSoftDeleteEntities = eventData.Context.ChangeTracker.Entries<IBaseSoftDeleteEntity>();
        foreach (var baseSoftDeleteEntity in baseSoftDeleteEntities)
        {
            DeleteBehavior(baseSoftDeleteEntity);
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private static void AddBehavior(EntityEntry<IBaseHistoricalEntity> baseHistoricalEntity)
    {
        baseHistoricalEntity.Property(p => p.UpdatedDate).IsModified = false;
        baseHistoricalEntity.Entity.CreatedDate = DateTime.Now;
    }

    private static void UpdateBehavior(EntityEntry<IBaseHistoricalEntity> baseHistoricalEntity)
    {
        baseHistoricalEntity.Property(p => p.CreatedDate).IsModified = false;
        baseHistoricalEntity.Entity.UpdatedDate = DateTime.Now;
    }

    private static void DeleteBehavior(EntityEntry<IBaseSoftDeleteEntity> baseSoftDeleteEntity)
    {
        baseSoftDeleteEntity.State = EntityState.Modified;
        baseSoftDeleteEntity.Property(p => p.IsDeleted).CurrentValue = true;
    }
}
