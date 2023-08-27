namespace Notiflow.IdentityServer.Data.Configurations.Users;

internal sealed class UserConfiguration : BaseHistoricalEntityConfiguration<User>
{
    public UserConfiguration() : base("getdate()")
    {
    }

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(User), table =>
        {
            table.HasCheckConstraint("CK_User_Email_Format", "Email LIKE '%_@__%.__%'");
        });

        builder.Property(p => p.Name).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.Surname).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(150).IsUnicode(true).IsRequired();
        builder.Property(p => p.Username).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.Password).HasMaxLength(200).IsUnicode(true).IsRequired();
        builder.Property(p => p.ProfilePhoto)
            .HasMaxLength(300).IsUnicode(true).IsRequired()
            .HasConversion(uriPhoto => uriPhoto.ToString(), stringPhoto => new Uri(stringPhoto));

        builder.HasOne(p => p.Tenant).WithMany(p => p.Users).HasForeignKey(p => p.TenantId).OnDelete(DeleteBehavior.Restrict);
    }
}
