namespace Notiflow.Backoffice.Persistence.Configurations.Customers;

internal sealed class CustomerConfiguration : BaseHistoricalSoftDeleteEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(p => p.Surname).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(10).IsFixedLength().IsUnicode(false).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.BirthDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.IsBlocked).HasDefaultValue(false).IsRequired();

        builder.Property(p => p.Gender).HasDefaultValue(Gender.None).IsRequired();
        builder.Property(p => p.MarriageStatus).HasDefaultValue(MarriageStatus.None).IsRequired();
    }
}