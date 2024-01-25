namespace Notiflow.IdentityServer.Controllers;

[Route("api/[controller]")]
public class BaseApiController : MainController
{
    public override IActionResult CreateActionResultInstance<T>(Result<T> result)
    {
        //var localizerService = HttpContext.RequestServices.GetRequiredService<ILocalizerService<ResultMessage>>();
        //result.Message = localizerService[1];

        return base.CreateActionResultInstance(result);
    }
}
