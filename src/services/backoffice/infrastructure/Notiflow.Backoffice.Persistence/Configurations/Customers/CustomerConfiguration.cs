namespace Notiflow.Backoffice.Persistence.Configurations.Customers;

internal sealed class CustomerConfiguration : BaseHistoricalSoftDeleteEntityConfiguration<Customer, int>
{
    public CustomerConfiguration() : base("now()", true)
    {

    }

    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(Customer).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_email_format", "email ~ '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$'");
            table.HasCheckConstraint("chk_phone_number_turkey", "phone_number ~ '^50|53|54|55|56\\d{8}$'");
            table.HasCheckConstraint("chk_tenant_id_restriction", "tenant_id > 0");
            table.HasCheckConstraint("chk_gender_value_limitation", "gender IN (1,2)");
            table.HasCheckConstraint("chk_marriage_status_value_limitation", "marriage_status IN (1,2)");
            table.HasCheckConstraint("chk_minimum_age_restriction", "birth_date >= '1950-01-01'");
        });

        builder.Property(p => p.Name).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(p => p.Surname).HasMaxLength(75).IsUnicode(false).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(10).IsFixedLength().IsUnicode(false).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(130).IsUnicode(false).IsRequired();
        builder.Property(p => p.Gender).HasConversion<int>().IsRequired();
        builder.Property(p => p.MarriageStatus).HasConversion<int>().IsRequired();
        builder.Property(p => p.BirthDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.IsBlocked).IsRequired();
        builder.Property(p => p.TenantId).IsRequired();
                         
        builder.HasIndex(p => new { p.Email, p.CreatedDate, p.TenantId }).IsDescending(false, true, false);
        builder.HasIndex(p => new { p.PhoneNumber, p.CreatedDate, p.TenantId }).IsDescending(false, true, false);
        builder.HasIndex(p => new { p.IsBlocked, p.IsDeleted, p.TenantId });
    }
}