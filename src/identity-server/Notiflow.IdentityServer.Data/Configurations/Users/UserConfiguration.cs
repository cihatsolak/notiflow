namespace Notiflow.IdentityServer.Data.Configurations.Users;

internal sealed class UserConfiguration : BaseHistoricalEntityConfiguration<User>
{
    public UserConfiguration() : base("getdate()")
    {
    }

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Username).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.Password).HasMaxLength(200).IsUnicode(true).IsRequired();

        builder.HasOne(p => p.Tenant).WithMany(p => p.Users).HasForeignKey(p => p.TenantId).OnDelete(DeleteBehavior.Restrict);
    }
}
