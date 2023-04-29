using Notiflow.IdentityServer.Core.Entities.Users;
using Puzzle.Lib.Entities.Entities.Base;

namespace Notiflow.IdentityServer.Data.Configurations.Users;

internal sealed class UserRefreshTokenConfiguration : BaseEntityConfiguration<UserRefreshToken>
{
    public UserRefreshTokenConfiguration() : base()
    {

    }

    public override void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.UserId).IsRequired(true);
        builder.Property(p => p.Token).HasMaxLength(200).IsRequired(true);
        builder.Property(p => p.ExpirationDate).HasDefaultValueSql("getdate()").IsRequired(true);

        builder.HasOne(p => p.User).WithOne(p => p.UserRefreshToken).HasForeignKey<UserRefreshToken>(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}
