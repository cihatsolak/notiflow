namespace Notiflow.Lib.Database.Configurations
{
    public class BaseHistoricalSoftDeleteEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseHistoricalSoftDeleteEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name.ToLowerInvariant(), DatabaseSchema.Dbo);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.CreatedDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()").IsRequired();

            builder.Property(p => p.IsDeleted).HasDefaultValue(false).IsRequired();
        }
    }
}
