namespace Notiflow.Backoffice.Persistence.Configurations.Histories;

internal class TextMessageHistoryConfiguration : BaseEntityConfiguration<TextMessageHistory, int>
{
    public TextMessageHistoryConfiguration() : base(true)
    {
    }

    public override void Configure(EntityTypeBuilder<TextMessageHistory> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(TextMessageHistory).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_textmessagehistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
        });

        builder.Property(p => p.Message).IsUnicode(false).IsRequired();
        builder.Property(p => p.IsSent).IsRequired();
        builder.Property(p => p.ErrorMessage).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.SentDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();

        builder.HasOne(p => p.Customer).WithMany(p => p.TextMessageHistories).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
