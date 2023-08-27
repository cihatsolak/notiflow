namespace Notiflow.IdentityServer.Data.Configurations.Users;

internal sealed class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>
{
    public RefreshTokenConfiguration() : base()
    {

    }

    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(RefreshToken), table =>
        {
            table.HasCheckConstraint("CK_RefreshToken_UserId", "[UserId] > 0");
        });

        builder.Property(p => p.UserId).IsRequired(true);
        builder.Property(p => p.Token).HasMaxLength(50).IsRequired(true);
        builder.Property(p => p.ExpirationDate).HasDefaultValueSql("getdate()").IsRequired(true);

        builder.HasOne(p => p.User).WithOne(p => p.RefreshToken).HasForeignKey<RefreshToken>(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}
