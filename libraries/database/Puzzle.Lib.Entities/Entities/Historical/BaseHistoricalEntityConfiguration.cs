namespace Puzzle.Lib.Entities.Entities.Historical
{
    public class BaseHistoricalEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseHistoricalEntity
    {
        private readonly string _defaultDateValueSql;
        private readonly bool _useLowerTableName;

        public BaseHistoricalEntityConfiguration(string defaultDateValueSql, bool useLowerTableName = false)
        {
            ArgumentException.ThrowIfNullOrEmpty(defaultDateValueSql);

            _defaultDateValueSql = defaultDateValueSql;
            _useLowerTableName = useLowerTableName;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            string tableName = _useLowerTableName ? typeof(TEntity).Name.ToLowerInvariant() : typeof(TEntity).Name;

            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.CreatedDate).ValueGeneratedOnAdd().HasDefaultValueSql(_defaultDateValueSql).IsRequired();
            builder.Property(p => p.UpdatedDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_defaultDateValueSql).IsRequired();
        }
    }
}
