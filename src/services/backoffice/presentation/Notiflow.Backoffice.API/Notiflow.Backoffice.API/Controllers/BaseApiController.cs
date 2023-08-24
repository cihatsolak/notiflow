namespace Notiflow.Backoffice.API.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public class BaseApiController : MainController
{
    private ISender _sender = null!;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}