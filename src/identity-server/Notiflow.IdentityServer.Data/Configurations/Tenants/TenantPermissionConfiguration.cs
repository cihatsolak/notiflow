namespace Notiflow.IdentityServer.Data.Configurations.Tenants;

internal sealed class TenantPermissionConfiguration : BaseHistoricalEntityConfiguration<TenantPermission>
{
    public TenantPermissionConfiguration() : base("getdate()")
    {
    }

    public override void Configure(EntityTypeBuilder<TenantPermission> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.IsSendMessagePermission).IsRequired();
        builder.Property(p => p.IsSendNotificationPermission).IsRequired();
        builder.Property(p => p.IsSendEmailPermission).IsRequired();

        builder.HasOne(p => p.Tenant).WithOne(p => p.TenantPermission).HasForeignKey<TenantPermission>(p => p.TenantId).OnDelete(DeleteBehavior.Restrict);
    }
}