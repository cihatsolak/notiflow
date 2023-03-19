namespace Puzzle.Lib.Database.Entities.Configurations
{
    public class BaseHistoricalEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseHistoricalEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name.ToLowerInvariant(), DatabaseSchema.Dbo);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.CreatedDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()").IsRequired();
        }
    }
}
