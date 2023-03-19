namespace Puzzle.Lib.Database.Entities.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name.ToLowerInvariant(), DatabaseSchema.Dbo);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
