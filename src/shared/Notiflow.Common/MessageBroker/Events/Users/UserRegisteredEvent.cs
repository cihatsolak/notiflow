namespace Notiflow.Common.MessageBroker.Events.Users;

public sealed record UserRegisteredEvent(string Email, string Message);
