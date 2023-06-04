namespace Notiflow.Backoffice.API.Controllers;

[Route("api/[controller]")]
public class BaseApiController : ResultController
{
    private ISender _sender = null!;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();


    private IStringLocalizer<Resource> _localizer = null!;
    protected IStringLocalizer<Resource> Localizer => _localizer ??= HttpContext.RequestServices.GetRequiredService<IStringLocalizer<Resource>>();

    
    protected override IActionResult CreateGetResultInstance<T>(Response<T> response)
    {
        CreateMessageByLanguage(response);
        return base.CreateGetResultInstance(response);
    }

    protected override IActionResult CreateOkResultInstance<T>(Response<T> response)
    {
        CreateMessageByLanguage(response);
        return base.CreateOkResultInstance(response);
    }

    protected override IActionResult CreateCreatedResultInstance<T>(Response<T> response, string actionName)
    {
        CreateMessageByLanguage(response);
        return base.CreateCreatedResultInstance(response, actionName);
    }

    protected override IActionResult CreateNoContentResultInstance<T>(Response<T> response)
    {
        CreateMessageByLanguage(response);
        return base.CreateNoContentResultInstance(response);
    }

    private void CreateMessageByLanguage<T>(Response<T> response)
    {
        if (response.Code == 0)
        {
            response.Message = Localizer["1000"];
        }
        else
        {
            response.Message = Localizer[response.Code.ToString()];
        }
    }
}