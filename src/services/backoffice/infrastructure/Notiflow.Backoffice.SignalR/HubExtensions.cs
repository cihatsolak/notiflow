namespace Notiflow.Backoffice.SignalR;

internal static class HubExtensions
{
    internal const string HUB_USERS = "signalr.hub.users";
    
    internal static string Id(this ClaimsPrincipal claimsPrincipal) 
        => claimsPrincipal.Claims.SingleOrDefault(claim => claim.Value == ClaimTypes.NameIdentifier)?.Value;
}
