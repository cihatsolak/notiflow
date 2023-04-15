namespace Notiflow.Backoffice.Persistence.Configurations.Customers;

internal sealed class CustomerConfiguration : BaseHistoricalSoftDeleteEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(Customer).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_gender_value_limitation", "gender IN (1,2)");
            table.HasCheckConstraint("chk_marriage_status_value_limitation", "marriage_status IN (1,2)");
            table.HasCheckConstraint("chk_minimum_age_restriction", "birth_date >= '1950-01-01'");
        });

        builder.Property(p => p.Name).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(p => p.Surname).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(10).IsFixedLength().IsUnicode(false).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.Gender).HasConversion<int>().IsRequired();
        builder.Property(p => p.MarriageStatus).HasConversion<int>().IsRequired();
        builder.Property(p => p.BirthDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.IsBlocked).HasDefaultValue(false).IsRequired();
    }
}