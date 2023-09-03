namespace Puzzle.Lib.Entities.Entities.SoftDelete
{
    public class BaseSoftDeleteEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseSoftDeleteEntity
    {
        private readonly bool _useLowerTableName;

        public BaseSoftDeleteEntityConfiguration()
        {
            _useLowerTableName = false;
        }

        public BaseSoftDeleteEntityConfiguration(bool useLowerTableName)
        {
            _useLowerTableName = useLowerTableName;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            string tableName = _useLowerTableName ? typeof(TEntity).Name.ToLowerInvariant() : typeof(TEntity).Name;

            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.IsDeleted).IsRequired();
        }
    }
}
