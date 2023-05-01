namespace Notiflow.Backoffice.Persistence.Configurations.Histories;

internal sealed class NotificationHistoryConfiguration : BaseEntityConfiguration<NotificationHistory>
{
    public NotificationHistoryConfiguration() : base(true)
    {
    }

    public override void Configure(EntityTypeBuilder<NotificationHistory> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(NotificationHistory).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_notificationhistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
        });

        builder.Property(p => p.Title).HasMaxLength(300).IsUnicode().IsRequired();
        builder.Property(p => p.Message).HasMaxLength(500).IsUnicode().IsRequired();
        builder.Property(p => p.IsSent).IsRequired();
        builder.Property(p => p.ErrorMessage).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.SentDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();

        builder.HasOne(p => p.Customer).WithMany(p => p.NotificationHistories).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}