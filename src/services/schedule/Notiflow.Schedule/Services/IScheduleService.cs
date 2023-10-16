namespace Notiflow.Schedule.Services;

public interface IScheduleService
{
    Task<ApiResponse<EmptyResponse>> MessageDeliveryAsync(ScheduleTextMessageRequest request);
}

public sealed class ScheduleManager : IScheduleService
{
    private readonly ScheduleDbContext _context;

    public async Task<ApiResponse<EmptyResponse>> MessageDeliveryAsync(ScheduleTextMessageRequest request)
    {
        
    }
}
