namespace Notiflow.Schedule.Controllers;

public class BaseApiController : MainController
{
    public override IActionResult CreateActionResultInstance(Result result)
    {
        var localizerService = HttpContext.RequestServices.GetRequiredService<ILocalizerService<ResultCodes>>();
        result.Message = localizerService[result.ResultCode];

        return base.CreateActionResultInstance(result);
    }

    public override IActionResult CreateActionResultInstance<T>(Result<T> result)
    {
        var localizerService = HttpContext.RequestServices.GetRequiredService<ILocalizerService<ResultCodes>>();
        result.Message = localizerService[result.ResultCode];

        return base.CreateActionResultInstance(result);
    }
}
