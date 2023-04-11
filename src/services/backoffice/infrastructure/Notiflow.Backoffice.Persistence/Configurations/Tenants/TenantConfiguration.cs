namespace Notiflow.Backoffice.Persistence.Configurations.Tenants
{
    internal sealed class TenantConfiguration : BaseHistoricalEntityConfiguration<Tenant>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(50).IsUnicode(false).IsRequired();
            builder.Property(p => p.Definition).HasMaxLength(200).IsUnicode(false).IsRequired();
            builder.Property(p => p.AppId).HasMaxLength(36).IsUnicode(false).IsFixedLength().IsRequired();
        }
    }
}
