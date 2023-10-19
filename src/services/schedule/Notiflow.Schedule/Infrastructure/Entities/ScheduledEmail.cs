namespace Notiflow.Schedule.Infrastructure.Entities;

public class ScheduledEmail : BaseEntity<int>
{
    public string Data { get; set; }
    public DateTime PlannedDeliveryDate { get; set; }
    public DateTime? SuccessDeliveryDate { get; set; }
    public DateTime? LastAttemptDate { get; set; }
    public int FailedAttempts { get; set; }
    public string ErrorMessage { get; set; }
    public bool IsSent { get; set; }
}
