﻿namespace Notiflow.Schedule.Infrastructure.Entities;

public sealed class ScheduledEmailEntityConfiguration : BaseEntityConfiguration<ScheduledEmail>
{
    public ScheduledEmailEntityConfiguration() : base()
    {
    }

    public override void Configure(EntityTypeBuilder<ScheduledEmail> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(ScheduledEmail), table =>
        {
            table.HasCheckConstraint("CK_FailedAttempts_Max", "[FailedAttempts] <= 3");
            table.HasCheckConstraint("CK_PlannedDeliveryDate_Min", "[PlannedDeliveryDate] >= DATEADD(MINUTE, 5, GETDATE())");
            table.HasCheckConstraint("CK_LastAttemptDate_PlannedDeliveryDate", "[LastAttemptDate] > [PlannedDeliveryDate]");
        });

        builder.Property(p => p.Data).IsUnicode(false).IsRequired();
        builder.Property(p => p.PlannedDeliveryDate).IsRequired();
        builder.Property(p => p.SuccessDeliveryDate);
        builder.Property(p => p.LastAttemptDate);
        builder.Property(p => p.FailedAttempts);
        builder.Property(p => p.ErrorMessage).HasMaxLength(200).IsUnicode(false);
        builder.Property(p => p.IsSent).HasDefaultValue(false);
    }
}