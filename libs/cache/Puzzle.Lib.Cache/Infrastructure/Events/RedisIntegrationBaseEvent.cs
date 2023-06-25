namespace Puzzle.Lib.Cache.Infrastructure.Events;

/// <summary>
/// Base class for events that are integrated with Redis.
/// </summary>
/// <remarks>
/// This class provides properties for Id and CreatedDate that are used to identify and timestamp the events.
/// </remarks>
public abstract class RedisIntegrationBaseEvent
{
    protected RedisIntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }

    protected RedisIntegrationBaseEvent(Guid id, DateTime createdDate)
    {
        Id = id;
        CreatedDate = createdDate;
    }

    public Guid Id { get; init; }
    public DateTime CreatedDate { get; init; }
}
