namespace Notiflow.Panel.Infrastructure.Handlers;

internal class TenantHandler : DelegatingHandler
{
    private const string tenantToken = "F50C77DB-6C3B-43E6-BEE2-2142A71E04E4";

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("x-tenant-token", tenantToken);

        return base.SendAsync(request, cancellationToken);
    }
}
