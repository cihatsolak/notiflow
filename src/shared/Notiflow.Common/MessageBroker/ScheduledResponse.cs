namespace Notiflow.Common.MessageBroker;

public sealed record ScheduledResponse
{
    [JsonRequired]
    public bool Succeeded { get; set; }
    
    [JsonRequired]
    public string ErrorMessage { get; set; }
}
