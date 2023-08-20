namespace Notiflow.Backoffice.API.Controllers;

[Route("api/[controller]")]
public class BaseApiController : MainController
{
    private ISender _sender = null!;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}