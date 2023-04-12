namespace Notiflow.Backoffice.Persistence.Configurations.Devices;

internal sealed class DeviceConfiguration : BaseHistoricalEntityConfiguration<Device>
{
    public override void Configure(EntityTypeBuilder<Device> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.OSVersion).HasDefaultValue(OSVersion.None).IsRequired();

        builder.HasOne(p => p.Customer).WithOne(p => p.Device).HasForeignKey<Device>(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
