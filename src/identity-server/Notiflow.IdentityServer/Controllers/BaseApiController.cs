namespace Notiflow.IdentityServer.Controllers;

[Route("api/[controller]")]
public class BaseApiController : MainController
{
    public override IActionResult CreateActionResultInstance<T>(Result<T> result)
    {
        var localizerService = HttpContext.RequestServices.GetRequiredService<ILocalizerService<ResultCodes>>();
        result.Message = localizerService[result.ResultCode];

        return base.CreateActionResultInstance(result);
    }
}
