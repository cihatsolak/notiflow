namespace Puzzle.Lib.Entities.Entities.Base
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        private readonly bool _useLowerTableName;

        public BaseEntityConfiguration(bool useLowerTableName = false)
        {
            _useLowerTableName = useLowerTableName;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            string tableName = _useLowerTableName ? typeof(TEntity).Name.ToLowerInvariant() : typeof(TEntity).Name;

            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
