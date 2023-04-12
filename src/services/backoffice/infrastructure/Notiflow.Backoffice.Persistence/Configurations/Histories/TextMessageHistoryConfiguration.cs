namespace Notiflow.Backoffice.Persistence.Configurations.Histories;

internal class TextMessageHistoryConfiguration : BaseEntityConfiguration<TextMessageHistory>
{
    public override void Configure(EntityTypeBuilder<TextMessageHistory> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Message).IsUnicode(false).IsRequired();

        builder.Property(p => p.IsSent).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.ErrorMessage).IsUnicode(false).IsRequired(false);
        builder.Property(p => p.SentDate).ValueGeneratedOnAdd().HasDefaultValueSql("now()").IsRequired();

        builder.HasOne(p => p.Customer).WithMany(p => p.TextMessageHistories).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
