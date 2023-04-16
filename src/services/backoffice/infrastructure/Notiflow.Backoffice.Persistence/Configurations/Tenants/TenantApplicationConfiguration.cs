namespace Notiflow.Backoffice.Persistence.Configurations.Tenants
{
    internal sealed class TenantApplicationConfiguration : BaseHistoricalEntityConfiguration<TenantApplication>
    {
        public override void Configure(EntityTypeBuilder<TenantApplication> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.FirebaseServerKey).HasMaxLength(152).IsUnicode(false).IsFixedLength().IsRequired();
            builder.Property(p => p.FirebaseSenderId).HasMaxLength(11).IsUnicode(false).IsFixedLength().IsRequired();

            builder.Property(p => p.HuaweiServerKey).HasMaxLength(44).IsUnicode(false).IsFixedLength().IsRequired();
            builder.Property(p => p.HuaweiSenderId).HasMaxLength(12).IsUnicode(false).IsFixedLength().IsRequired();

            builder.Property(p => p.MailFromAddress).HasMaxLength(200).IsUnicode(false).IsRequired();
            builder.Property(p => p.MailFromName).HasMaxLength(150).IsUnicode(false).IsRequired();
            builder.Property(p => p.MailReplyAddress).HasMaxLength(200).IsUnicode(false).IsRequired();

            builder.HasOne(p => p.Tenant).WithOne(p => p.TenantApplication).HasForeignKey<TenantApplication>(p => p.TenantId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}