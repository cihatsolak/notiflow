﻿namespace Puzzle.Lib.Entities.Entities.HistoricalSoftDelete
{
    public class BaseHistoricalSoftDeleteEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseHistoricalSoftDeleteEntity
    {
        private readonly string _defaultDateValueSql;
        private readonly bool _useLowerTableName;

        public BaseHistoricalSoftDeleteEntityConfiguration(string defaultDateValueSql, bool useLowerTableName = false)
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

            builder.Property(p => p.IsDeleted).IsRequired();
        }
    }
}
