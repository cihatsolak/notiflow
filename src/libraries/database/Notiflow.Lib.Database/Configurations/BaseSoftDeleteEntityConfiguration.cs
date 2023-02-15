namespace Notiflow.Lib.Database.Configurations
{
    public class BaseSoftDeleteEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseSoftDeleteEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name.ToLowerInvariant(), DatabaseSchema.Default);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.IsDeleted).HasDefaultValue(false).IsRequired();
        }
    }
}
