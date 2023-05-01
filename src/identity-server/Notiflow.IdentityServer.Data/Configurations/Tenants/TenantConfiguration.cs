namespace Notiflow.IdentityServer.Data.Configurations.Tenants;

internal sealed class TenantConfiguration : BaseHistoricalEntityConfiguration<Tenant>
{
    public TenantConfiguration() : base("getdate()")
    {
    }

    public override void Configure(EntityTypeBuilder<Tenant> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.Definition).HasMaxLength(300).IsUnicode(false).IsRequired();
        builder.Property(p => p.Token).HasMaxLength(36).IsUnicode(false).IsFixedLength().IsRequired();
    }
}
