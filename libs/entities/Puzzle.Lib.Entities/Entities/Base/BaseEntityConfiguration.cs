namespace Puzzle.Lib.Entities.Entities.Base;

public class BaseEntityConfiguration<TEntity, TPrimaryKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TPrimaryKey>
                                                                                               where TPrimaryKey : notnull
{
    private readonly bool _useLowerTableName;

    public BaseEntityConfiguration()
    {
        _useLowerTableName = false;
    }

    public BaseEntityConfiguration(bool useLowerTableName)
    {
        _useLowerTableName = useLowerTableName;
    }

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        string tableName = _useLowerTableName ? typeof(TEntity).Name.ToLowerInvariant() : typeof(TEntity).Name;

        builder.ToTable(tableName);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd(); //veri tabanı otonatik olarak primary key değerini oluştur.r
    }
}
