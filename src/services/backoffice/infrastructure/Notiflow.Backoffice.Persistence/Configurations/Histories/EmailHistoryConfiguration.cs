namespace Notiflow.Backoffice.Persistence.Configurations.Histories;

internal sealed class EmailHistoryConfiguration : BaseEntityConfiguration<EmailHistory>
{
    public EmailHistoryConfiguration() : base(true)
    {
    }

    public override void Configure(EntityTypeBuilder<EmailHistory> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(EmailHistory).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_emailhistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
        });

        builder.Property(p => p.Cc).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.Bcc).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.Subject).HasMaxLength(300).IsUnicode(false).IsRequired();
        builder.Property(p => p.Body).IsUnicode(true).IsRequired();
        builder.Property(p => p.IsSent).IsRequired();
        builder.Property(p => p.ErrorMessage).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.SentDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();

        builder.HasOne(p => p.Customer).WithMany(p => p.EmailHistories).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
