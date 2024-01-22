namespace Notiflow.IdentityServer.Data.Configurations.Users;

internal sealed class UserOutboxConfiguration : IEntityTypeConfiguration<UserOutbox>
{
    public void Configure(EntityTypeBuilder<UserOutbox> builder)
    {
        builder.ToTable(nameof(UserOutbox), table =>
        {
            table.HasCheckConstraint("CK_UserOutbox_ProcessedDate", "IsProcessed = 1 AND ProcessedDate IS NULL");
        });

        builder.HasKey(p => p.IdempotentToken);

        builder.Property(p => p.IdempotentToken)
            .IsRequired();

        builder.Property(p => p.MessageType)
            .IsUnicode(false)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Payload)
            .IsUnicode(true)
            .IsRequired()
            .HasMaxLength(2500);            

        builder.Property(p => p.CreatedDate)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("getdate()")
            .IsRequired();

        builder.Property(p => p.ProcessedDate)
            .IsRequired(false);

        builder.Property(p => p.IsProcessed)
            .HasDefaultValue(false)
            .IsRequired();
    }
}
