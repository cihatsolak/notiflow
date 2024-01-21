﻿namespace Notiflow.Schedule.Infrastructure.Entities;

public class ScheduledTextMessage : BaseEntity<int>
{
    public string Data { get; set; }
    public DateTime PlannedDeliveryDate { get; set; }
    public DateTime? SuccessDeliveryDate { get; set; }
    public DateTime? LastAttemptDate { get; set; }
    public int FailedAttempts { get; set; }
    public string ErrorMessage { get; set; }
    public bool IsSent { get; set; }
}

public sealed class ScheduledTextMessageEntityConfiguration : BaseEntityConfiguration<ScheduledTextMessage, int>
{
    public ScheduledTextMessageEntityConfiguration() : base()
    {
    }

    public override void Configure(EntityTypeBuilder<ScheduledTextMessage> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(ScheduledTextMessage), table =>
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