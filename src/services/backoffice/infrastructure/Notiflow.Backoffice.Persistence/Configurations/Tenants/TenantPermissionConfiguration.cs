namespace Notiflow.Backoffice.Persistence.Configurations.Tenants
{
    internal sealed class TenantPermissionConfiguration : BaseHistoricalEntityConfiguration<TenantPermission>
    {
        public override void Configure(EntityTypeBuilder<TenantPermission> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.IsSendMessagePermission).HasDefaultValue(false).IsRequired();
            builder.Property(p => p.IsSendNotificationPermission).HasDefaultValue(false).IsRequired();
            builder.Property(p => p.IsSendEmailPermission).HasDefaultValue(false).IsRequired();

            builder.HasOne(p => p.Tenant)
                .WithOne(p => p.TenantPermission)
                .HasForeignKey<TenantPermission>(p => p.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}