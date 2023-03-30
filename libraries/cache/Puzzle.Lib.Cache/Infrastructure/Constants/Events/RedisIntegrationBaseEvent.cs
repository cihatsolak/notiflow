namespace Puzzle.Lib.Cache.Infrastructure.Constants.Events
{
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
}
