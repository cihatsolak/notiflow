namespace Notiflow.Backoffice.Persistence.Configurations.Devices;

internal sealed class DeviceConfiguration : BaseHistoricalEntityConfiguration<Device>
{
    public override void Configure(EntityTypeBuilder<Device> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(Device).ToLowerInvariant(), table =>
        {
            table.HasCheckConstraint("CK_Device_CloudMessagePlatformCanTakeValues", "CloudMessagePlatform IN (1, 2)");
        });

        builder.Property(p => p.OSVersion).HasDefaultValue(OSVersion.None).IsRequired();
        builder.Property(p => p.Code).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(p => p.Token).HasMaxLength(180).IsUnicode(false).IsRequired();
        builder.Property(p => p.CloudMessagePlatform).HasConversion<int>().HasDefaultValue(CloudMessagePlatform.Firesabe).IsRequired();

        builder.HasOne(p => p.Customer).WithOne(p => p.Device).HasForeignKey<Device>(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
