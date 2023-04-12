namespace Notiflow.Backoffice.Persistence.Configurations.Histories;

internal sealed class EmailHistoryConfiguration : BaseEntityConfiguration<EmailHistory>
{
    public override void Configure(EntityTypeBuilder<EmailHistory> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Recipients).IsUnicode(false).IsRequired();
        builder.Property(p => p.Cc).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.Bcc).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.Subject).IsUnicode(false).IsRequired();
        builder.Property(p => p.Body).IsUnicode(true).IsRequired();
        builder.Property(p => p.IsSent).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.ErrorMessage).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.SentDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();

        builder.HasOne(p => p.Customer).WithMany(p => p.EmailHistories).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
