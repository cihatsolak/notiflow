namespace Notiflow.Backoffice.Persistence.Configurations.Histories;

internal sealed class NotificationHistoryConfiguration : BaseEntityConfiguration<NotificationHistory>
{
    public override void Configure(EntityTypeBuilder<NotificationHistory> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(NotificationHistory).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_senddate_greaterthan_createddate", "send_date >= created_date");
            table.HasCheckConstraint("chk_emailhistory_issent_errormessage", "(is_send = 0 and error_message IS NOT NULL) OR (is_send = 1 and error_message IS NULL)");
        });

        builder.Property(p => p.Title).HasMaxLength(250).IsUnicode().IsRequired();
        builder.Property(p => p.Message).HasMaxLength(500).IsUnicode().IsRequired();
        builder.Property(p => p.IsSent).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.ErrorMessage).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.SentDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();

        builder.HasOne(p => p.Customer).WithMany(p => p.NotificationHistories).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}