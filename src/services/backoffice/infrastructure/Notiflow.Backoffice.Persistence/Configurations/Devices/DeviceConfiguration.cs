namespace Notiflow.Backoffice.Persistence.Configurations.Devices;

internal sealed class DeviceConfiguration : BaseHistoricalEntityConfiguration<Device, int>
{
    public DeviceConfiguration() : base("now()", true)
    {
    }

    public override void Configure(EntityTypeBuilder<Device> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(Device).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("chk_os_version_value_limitation", "os_version IN (1,2, 3, 4)");
            table.HasCheckConstraint("chk_cloud_message_platform_value_limitation", "cloud_message_platform IN (1,2)");
        });

        builder.Property(p => p.OSVersion).HasConversion<int>().IsRequired();
        builder.Property(p => p.Code).HasMaxLength(150).IsUnicode(false).IsRequired();
        builder.Property(p => p.Token).HasMaxLength(250).IsUnicode(false).IsRequired();
        builder.Property(p => p.CloudMessagePlatform).HasConversion<int>().IsRequired();

        builder.HasOne(p => p.Customer).WithOne(p => p.Device).HasForeignKey<Device>(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
