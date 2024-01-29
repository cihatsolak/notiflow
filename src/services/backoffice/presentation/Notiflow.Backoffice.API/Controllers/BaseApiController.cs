namespace Notiflow.Backoffice.API.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public class BaseApiController : MainController
{
    private ISender _sender = null!;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public override IActionResult CreateActionResultInstance<T>(Result<T> result)
    {
        var localizerService = HttpContext.RequestServices.GetRequiredService<ILocalizerService<ResultCodes>>();
        result.Message = localizerService[result.ResultCode];

        return base.CreateActionResultInstance(result);
    }
}