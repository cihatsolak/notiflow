namespace Notiflow.Backoffice.Persistence.Configurations.Histories;

internal sealed class EmailHistoryConfiguration : BaseEntityConfiguration<EmailHistory>
{
    public override void Configure(EntityTypeBuilder<EmailHistory> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(EmailHistory).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_senddate_greaterthan_createddate", "send_date >= created_date");
            table.HasCheckConstraint("chk_emailhistory_issent_errormessage", "(is_send = 0 and error_message IS NOT NULL) OR (is_send = 1 and error_message IS NULL)");
        });

        builder.Property(p => p.Recipients).IsUnicode(false).IsRequired();
        builder.Property(p => p.Cc).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.Bcc).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.Subject).HasMaxLength(200).IsUnicode(false).IsRequired();
        builder.Property(p => p.Body).IsUnicode(true).IsRequired();
        builder.Property(p => p.IsSent).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.ErrorMessage).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.SentDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();

        builder.HasOne(p => p.Customer).WithMany(p => p.EmailHistories).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
