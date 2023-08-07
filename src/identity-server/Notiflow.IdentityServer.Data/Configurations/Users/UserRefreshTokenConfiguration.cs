namespace Notiflow.IdentityServer.Data.Configurations.Users;

internal sealed class UserRefreshTokenConfiguration : BaseEntityConfiguration<UserRefreshToken>
{
    public UserRefreshTokenConfiguration() : base()
    {

    }

    public override void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(UserRefreshToken), table =>
        {
            table.HasCheckConstraint("CK_UserRefreshToken_MailSmtpPort", "[UserId] > 0");
        });

        builder.Property(p => p.UserId).IsRequired(true);
        builder.Property(p => p.Token).HasMaxLength(50).IsRequired(true);
        builder.Property(p => p.ExpirationDate).HasDefaultValueSql("getdate()").IsRequired(true);

        builder.HasOne(p => p.User).WithOne(p => p.UserRefreshToken).HasForeignKey<UserRefreshToken>(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}
